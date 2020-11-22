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
    class LicenseRepository : IRepository<License>
    {
        public ObservableCollection<License> GetAll()
        {
            ObservableCollection<License> licenses = new ObservableCollection<License>();
            DataTable licenseTable = DatabaseUtility.GetTable("SELECT * FROM BookLicense");
            foreach (DataRow row in licenseTable.Rows)
            {
                licenses.Add(new License()
                {
                    Id = row.Field<int>("Id"),
                    LicenseState = row.Field<string>("License")
                });
            }

            return licenses;
        }


        public License Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(License model)
        {
            DatabaseUtility.Exec("INSERT INTO BookLicense (License) VALUES (@LicenseState)", new List<SqlParameter>()
            {
                new SqlParameter("@LicenseState", model.LicenseState)
            });
        }

        public void Update(License model)
        {
            DatabaseUtility.Exec("UPDATE BookLicense SET  License = @LicenseState  WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@LicenseState", model.LicenseState)
            });
        }

        public void Delete(License model)
        {
            DatabaseUtility.Exec("DELETE FROM BookLicense WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Состояние лицензии", DisplayMemberBinding = new Binding() { Path = new PropertyPath("LicenseState") } });
            return gridView;
        }
    }
}
