using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        public void Set(User model)
        {
            throw new NotImplementedException();
        }

        public void Update(User model)
        {
            DatabaseUtility.Exec("UPDATE BookUsers SET Name=@Name, Position=@Position, UserName=@UserName, AccessToRegion=@AccessToRegion, AccessToBook=@AccessToBook WHERE Id=@Id", new List<SqlParameter>
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@Name", (object)model.Name ?? DBNull.Value),
                new SqlParameter("@Position", (object)model.Position ?? DBNull.Value),
                //new SqlParameter("@UserName", (object)model.UserName ?? DBNull.Value),
                new SqlParameter("@AccessToRegion", model.AccessToRegion),
                new SqlParameter("@AccessToBook", model.AccessToBook),
            });
        }

        public void Delete(User model)
        {
            DatabaseUtility.Exec("DELETE FROM BookUsers WHERE Id = @Id",
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
