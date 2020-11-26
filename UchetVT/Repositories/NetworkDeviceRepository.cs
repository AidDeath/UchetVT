using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UchetVT.Repositories
{
    class NetworkDeviceRepository : IRepository<NetworkDevice>
    {
        private IRepository<NetworkDevice> _repositoryImplementation;

        public ObservableCollection<NetworkDevice> GetAll()
        {
            ObservableCollection<NetworkDevice> networkDevices = new ObservableCollection<NetworkDevice>();
            DataTable networkDevicesTable = DatabaseUtility.GetTable("SELECT * FROM BookNetworkDevice");
            foreach (DataRow row in networkDevicesTable.Rows)
            {
                networkDevices.Add(new NetworkDevice()
                {
                    Id = row.Field<int>("Id"),
                    NameNetworkDevice = row.Field<string>("NameNetworkDevice")
                });
            }

            return networkDevices;
        }

        public NetworkDevice Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(NetworkDevice model)
        {
            DatabaseUtility.Exec("INSERT INTO BookNetworkDevice (NameNetworkDevice) VALUES (@NameNetworkDevice)", new List<SqlParameter>()
            {
                new SqlParameter("@NameNetworkDevice", model.NameNetworkDevice)
            });
        }

        public void Update(NetworkDevice model)
        {
            DatabaseUtility.Exec("UPDATE BookNetworkDevice SET  NameNetworkDevice = @NameNetworkDevice  WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@NameNetworkDevice", model.NameNetworkDevice)
            });
        }

        public void Delete(NetworkDevice model)
        {
            DatabaseUtility.Exec("DELETE FROM BookNetworkDevice WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Наименование устройства", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameNetworkDevice") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Код назначения", DisplayMemberBinding = new Binding() { Path = new PropertyPath("IdAssign") } });
            return gridView;
        }
    }
}
