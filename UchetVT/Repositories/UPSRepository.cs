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
    class UPSRepository : IRepository<UPS>
    {
        private IRepository<UPS> _repositoryImplementation;

        public ObservableCollection<UPS> GetAll()
        {
            ObservableCollection<UPS> upses = new ObservableCollection<UPS>();
            DataTable upsesTable = DatabaseUtility.GetTable("SELECT * FROM BookUPS");
            foreach (DataRow row in upsesTable.Rows)
            {
                upses.Add(new UPS()
                {
                    Id = row.Field<int>("Id"),
                    NameUPS = row.Field<string>("NameUPS")
                });
            }

            return upses;
        }


        public UPS Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(UPS model)
        {
            DatabaseUtility.Exec("INSERT INTO BookUPS (NameUPS) VALUES (@NameUPS)", new List<SqlParameter>()
            {
                new SqlParameter("@NameUPS", model.NameUPS)
            });
        }

        public void Update(UPS model)
        {
            DatabaseUtility.Exec("UPDATE BookUPS SET  NameUPS = @NameUPS  WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@NameUPS", model.NameUPS)
            });
        }

        public void Delete(UPS model)
        {
            DatabaseUtility.Exec("DELETE FROM BookUPS WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Модель ИБП", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameUPS") } });
            return gridView;
        }
    }
}
