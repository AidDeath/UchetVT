using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace UchetVT
{
    /// <summary>
    /// Interaction logic for LogonWindow.xaml
    /// </summary>
    public partial class LogonWindow : Window
    {
        public LogonWindow()
        {
            InitializeComponent();
            UsernameTextBox.Focus();

        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                UserName = this.UsernameTextBox.Text,
                UserPassword = this.UserPasswordBox.Password
            };




            //SqlConnection connection = new ServerConnection().ConnectToServer(user);

            try
            {
                string ConnectionString =
                    $"Data Source = {Properties.Settings.Default.ServerHostname}\\{Properties.Settings.Default.SQLInstance};" +
                    $" Persist Security Info = True; " +
                    $"Password = {user.UserPassword};" +
                    $" User ID = {user.UserName};" +
                    $" Initial Catalog = {Properties.Settings.Default.Database}";

                DatabaseUtility.OpenConnection(ConnectionString);


                if (System.Windows.Application.Current.Properties["ConnectionString"] != null)
                    System.Windows.Application.Current.Properties.Remove("ConnectionString");
                System.Windows.Application.Current.Properties.Add("ConnectionString", ConnectionString);

                //Получаем из БД информацию о пользователе
                var extUserDataTable = DatabaseUtility.GetTable("SELECT TOP 1 * FROM dbo.fn_t_GetExtendedUserData()");
                if (extUserDataTable.Rows.Count == 0)
                {
                    Exception ex = new Exception("У пользователя нет доступа к данным ни одного района");
                    throw ex;
                }

                MainWindow main = new MainWindow();
                main.CurrentUser = user;
                main.CurrentUser.Id = extUserDataTable.Rows[0].Field<int>("Id");
                main.CurrentUser.AccessToRegion = extUserDataTable.Rows[0].Field<string>("AccessToRegion");
                main.CurrentUser.AccessToBook = extUserDataTable.Rows[0].Field<string>("AccessToBook");
                main.CurrentUser.Name = extUserDataTable.Rows[0].Field<string>("Name");

                System.Windows.Application.Current.Properties.Add("CurrentUserId", main.CurrentUser.Id);

                this.Close();
                main.Show();

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 18456:
                        {
                            MessageBox.Show("Неверное имя пользователя или пароль");
                            break;
                        }

                    case -1:
                        {
                            MessageBox.Show("Не удалось найти сервер MSSQL, указанный в настройках\nПроверьте сеть и файл app.config ");
                            break;
                        }

                    case 4060:
                        {
                            MessageBox.Show("Не удалось найти БД, указанную в настройках\nПроверьте файл app.config ");
                            break;
                        }

                    default:
                        {
                            throw ex;
                        }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
