using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UchetVT
{
    class UserRepository : IRepository<User>
    {
        public ObservableCollection<User> GetAll()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            DataTable usersTable = DatabaseUtility.GetTable("SELECT * FROM BookUsers");
            foreach (DataRow row in usersTable.Rows)
            {
                users.Add(new User()
                {
                    Id = row.Field<int>("Id"),
                    Name = row.Field<string>("Name"),
                    Position = row.Field<string>("Position"),
                    UserName = row.Field<string>("UserName"),
                    AccessToRegion = row.Field<string>("AccessToRegion"),
                    AccessToBook = row.Field<string>("AccessToBook")
                });
            }

            return users;

        }


        public User Get(int id)
        {
            DataTable userTable = DatabaseUtility.GetTable("SELECT * FROM BookUsers WHERE Id=@Id", new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            });

            return new User
            {
                Id = userTable.Rows[0].Field<int>("Id"),
                Name = userTable.Rows[0].Field<string>("Name"),
                Position = userTable.Rows[0].Field<string>("Position"),
                UserName = userTable.Rows[0].Field<string>("UserName"),
                AccessToRegion = userTable.Rows[0].Field<string>("AccessToRegion"),
                AccessToBook = userTable.Rows[0].Field<string>("AccessToBook"),
            };
        }

        public void Set(User model)
        {
            try
            {
                //Проверяем, есть ли уже такой логин в СУБД , если нет - запрашиваем для нового пароль, он будет создан
                switch (DatabaseUtility.ExecStorageProcWithReturnValue("sp_CheckLogin", new List<SqlParameter>
                {
                    new SqlParameter("@UserName", model.UserName)
                }))
                {

                    case -1:  // нет ни логина ни пользователя
                    case 1:     //нет логина
                        {
                            PasswordWindow pwdWindow = new PasswordWindow();
                            foreach (Window window in System.Windows.Application.Current.Windows)
                                if (window is RecordWindow)
                                    pwdWindow.Owner = window;
                            if (pwdWindow.ShowDialog() == true)
                            {
                                model.UserPassword = pwdWindow.Password;
                            }
                            else throw new Exception("PwdEnterCancelled");
                            break;
                        }
                    case 0:    //нет пользователя
                        {
                            break;
                        }
                    case 2: //есть и пользователь и логин
                        {
                            MessageBox.Show("Такой пользователь уже есть", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            break;
                        }

                }

                DatabaseUtility.ExecStorageProc("sp_CreateUserMSSQL2005",
                    new List<SqlParameter>
                    {
                        new SqlParameter("@Name", (object) model.Name ?? DBNull.Value),
                        new SqlParameter("@Position", (object) model.Position ?? DBNull.Value),
                        new SqlParameter("@UserName", model.UserName),
                        new SqlParameter("@Password", (object) model.UserPassword ?? DBNull.Value),
                        new SqlParameter("@AccessToRegion", (object) model.AccessToRegion ?? DBNull.Value),
                        new SqlParameter("@AccessToBook", (object) model.AccessToBook ?? DBNull.Value)
                    });

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message == "PwdEnterCancelled" ? "Создание пользователя отменено" : e.Message);
            }

        }

        public void Update(User model)
        {
            User dbUserData = Get(model.Id);

            if (dbUserData.UserName == model.UserName)
            {
                DatabaseUtility.Exec("UPDATE BookUsers SET Name=@Name, Position=@Position, AccessToRegion=@AccessToRegion, AccessToBook=@AccessToBook WHERE Id=@Id", new List<SqlParameter>
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@Name", (object)model.Name ?? DBNull.Value),
                new SqlParameter("@Position", (object)model.Position ?? DBNull.Value),
                //new SqlParameter("@UserName", (object)model.UserName ?? DBNull.Value),
                new SqlParameter("@AccessToRegion", model.AccessToRegion),
                new SqlParameter("@AccessToBook", model.AccessToBook),
            });
                if (dbUserData.AccessToBook.Equals(model.AccessToBook) ||
                    (dbUserData.AccessToRegion.Equals(model.AccessToRegion)))
                    MessageBox.Show("Права доступа будут применены \nпри следующем входе");
            }
            else
            {
                if (dbUserData.Id.ToString() == Application.Current.Properties["CurrentUserId"].ToString())
                {
                    MessageBox.Show("Недопустимо изменение собственного имени входа", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (MessageBox.Show($"Пользователь будет пересоздан,\n так как изменилось имя входа в систему \nУдалить старое имя входа?",
                        "Пересоздание пользователя",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
                    MessageBoxResult.Yes) Delete(model);        // Удаляем логин старого пользователя TODO: ДОРАБОТАТЬ! Вместо DELETE удалять логин
                Set(model);     // создаём нового пользователя 

                //TODO: ДОРАБОТАТЬ! Вместо DELETE удалять логин
            }
        }

        public void Delete(User model)
        {
            if (model.Id.ToString() == Application.Current.Properties["CurrentUserId"].ToString())
            {
                MessageBox.Show("Нельзя удалить собственное имя пользователя во время сесссии",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Удалить пользователя " + model.UserName + " ?",
                    "Удаление пользователя", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes) DatabaseUtility.Exec("DELETE FROM BookUsers WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "ФИО", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Name") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Должность", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Position") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Имя входа", DisplayMemberBinding = new Binding() { Path = new PropertyPath("UserName") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Доступ к данным районов", DisplayMemberBinding = new Binding() { Path = new PropertyPath("AccessToRegion") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Доступ к справочникам", DisplayMemberBinding = new Binding() { Path = new PropertyPath("AccessToBook") } });

            return gridView;
        }

    }
}
