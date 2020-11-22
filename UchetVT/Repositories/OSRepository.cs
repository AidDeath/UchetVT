using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UchetVT
{
    class OSRepository : IRepository<OS>
    {
        public ObservableCollection<OS> GetAll()
        {
            ObservableCollection<OS> oses = new ObservableCollection<OS>();
            DataTable osTable = DatabaseUtility.GetTable("SELECT * FROM BookOS");
            foreach (DataRow row in osTable.Rows)
            {
                oses.Add(new OS()
                {
                    Id = row.Field<int>("Id"),
                    NameOS = row.Field<string>("NameOS")
                });
            }

            return oses;
        }


        public OS Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(OS model)
        {
            DatabaseUtility.Exec("INSERT INTO BookOS (NameOS) VALUES (@NameOS)", new List<SqlParameter>()
            {
                new SqlParameter("@NameOS", model.NameOS)
            });
        }

        public void Update(OS model)
        {
            DatabaseUtility.Exec("UPDATE BookOS SET  NameOS = @NameOS  WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@NameOS", model.NameOS)
            });
        }

        public void Delete(OS model)
        {
            DatabaseUtility.Exec("DELETE FROM BookOS WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Наименование ОС", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameOS") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Год выхода", DisplayMemberBinding = new Binding() { Path = new PropertyPath("YearOut") } });
            return gridView;
        }
    }
}
