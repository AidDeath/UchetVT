using System.Windows;

namespace UchetVT
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public string Password { get; set; }
        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Pass.Password))
            {
                Password = Pass.Password;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Пароль не может быть пустым", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Pass.Password = "";
            }
        }
    }
}
