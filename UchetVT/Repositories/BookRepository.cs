using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UchetVT
{
    class BookRepository : IRepository<Book>
    {
        public ObservableCollection<Book> GetAll()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            DataTable bookTable = DatabaseUtility.GetTable("SELECT * FROM BookBooks");
            foreach (DataRow row in bookTable.Rows)
            {
                books.Add(new Book()
                {
                    Id = row.Field<int>("Id"),
                    NameBook = row.Field<string>("NameBook")
                });
            }

            return books;
        }


        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(Book model)
        {
            throw new NotImplementedException();
        }

        public void Update(Book model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book model)
        {
            throw new NotImplementedException();
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") }});
            gridView.Columns.Add(new GridViewColumn() { Header = "Справочник", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameBook") } });
            return gridView;
        }
    }
}
