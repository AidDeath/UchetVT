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
    class RegionRepository : IRepository<Region>
    {
        public ObservableCollection<Region> GetAll()
        {
            ObservableCollection<Region> regions = new ObservableCollection<Region>();
            DataTable regionTable = DatabaseUtility.GetTable("SELECT * FROM BookRegion WHERE Id IN(" + System.Windows.Application.Current.Properties["AccessToRegion"].ToString() + ")");
            foreach (DataRow row in regionTable.Rows)
            {
                regions.Add(new Region()
                {
                    Id = row.Field<int>("Id"),
                    FinId = row.Field<int>("FinId"),
                    NameFD = row.Field<string>("NameFD"),
                    NameCity = row.Field<string>("NameCity")
                });
            }

            return regions;
        }


        public Region Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(Region model)
        {
            throw new NotImplementedException();
        }

        public void Update(Region model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Region model)
        {
            DatabaseUtility.Exec("DELETE FROM BookRegion WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Финорган", DisplayMemberBinding = new Binding() { Path = new PropertyPath("FinId") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Наименование ФО", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameFD") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Город", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameCity") } });

            return gridView;
        }
    }
}
