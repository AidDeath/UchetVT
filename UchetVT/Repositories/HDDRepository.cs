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
    class HDDRepository : IRepository<HDD>
    {
        public ObservableCollection<HDD> GetAll()
        {
            ObservableCollection<HDD> hdds = new ObservableCollection<HDD>();
            DataTable HDDsTable = DatabaseUtility.GetTable("SELECT * FROM BookHdd");
            foreach (DataRow row in HDDsTable.Rows)
            {
                hdds.Add(new HDD()
                {
                    Id = row.Field<int>("Id"),
                    NameHDD = row.Field<string>("NameHDD")
                });
            }

            return hdds;
        }


        public HDD Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(HDD model)
        {
            DatabaseUtility.Exec("INSERT INTO BookHDD (NameHDD) VALUES (@NameHDD)", new List<SqlParameter>()
            {
                new SqlParameter("@NameHDD", model.NameHDD)
            });
        }

        public void Update(HDD model)
        {
            DatabaseUtility.Exec("UPDATE BookHDD SET  NameHDD = @NameHDD   WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@NameHDD", model.NameHDD)
            });
        }

        public void Delete(HDD model)
        {
            DatabaseUtility.Exec("DELETE FROM BookHDD WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Модель жесткого диска", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameHDD") } });

            return gridView;
        }
    }
}
