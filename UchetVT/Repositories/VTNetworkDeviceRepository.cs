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
    class VTNetworkDeviceRepository : IVtRepository<VTNetworkDevice>
    {
        public ObservableCollection<VTNetworkDevice> GetAll(int regionId)
        {
            ObservableCollection<VTNetworkDevice> vtNetworkDevices = new ObservableCollection<VTNetworkDevice>();
            DataTable vtNetworkDevicesTable = (regionId != 25) ? DatabaseUtility.GetTable("SELECT * FROM v_GetNetworkDevices WHERE OwnerRegion IN (" + regionId + ")") :
                DatabaseUtility.GetTable("SELECT * FROM v_GetNetworkDevices");

            foreach (DataRow row in vtNetworkDevicesTable.Rows)
            {
                VTNetworkDevice vtNetworkDevice = new VTNetworkDevice()
                {
                    Id = row.Field<int>("Id"),
                    DeviceId = row.Field<int>("DeviceId"),
                    NetworkDevice = new NetworkDevice() { Id = row.Field<int>("DeviceId"), NameNetworkDevice = row.Field<string>("NameNetworkDevice") },
                    // ниже общие поля для всех моделей VT
                    YearUsingSince = row.Field<int>("YearUsingSince"),
                    InventoryNo = row.Field<string>("InventoryNo"),
                    SerialNumber = row.Field<string>("SerialNumber"),
                    Ip = row.Field<string>("Ip"),
                    Room = row.Field<string>("Room"),
                    Note = row.Field<string>("Note"),
                    InUse = row.Field<bool>("InUse"),
                    OwnerRegion = row.Field<int>("OwnerRegion")
                };

                vtNetworkDevices.Add(vtNetworkDevice);
            }

            return vtNetworkDevices;

        }

        public void Set(VTNetworkDevice model)
        {
            DatabaseUtility.Exec(
                "INSERT INTO VTNetworkDevice (DeviceId, YearUsingSince, InventoryNo, SerialNumber, Ip, Room, Note, OwnerRegion, InUse, DateEdit) VALUES (" +
                "@DeviceId, @YearUsingSince, @InventoryNo, @SerialNumber, @Ip, @Room, @Note, @OwnerRegion, @InUse, GETDATE())",
                new List<SqlParameter>()
                {
                    new SqlParameter("@DeviceId", model.DeviceId),
                    new SqlParameter("@YearUsingSince", model.YearUsingSince),
                    new SqlParameter("@InventoryNo", (object)model.InventoryNo ?? DBNull.Value),
                    new SqlParameter("@SerialNumber",(object)model.SerialNumber ?? DBNull.Value),
                    new SqlParameter("@Ip", (object)model.Ip ?? DBNull.Value),
                    new SqlParameter("@Room", (object)model.Room ?? DBNull.Value),
                    new SqlParameter("@Note", (object)model.Note ?? DBNull.Value),
                    new SqlParameter("@OwnerRegion", model.OwnerRegion),
                    new SqlParameter("@InUse", model.InUse)
                });
        }

        public void Update(VTNetworkDevice model)
        {
            DatabaseUtility.Exec("UPDATE VTNetworkDevice SET " +
                                 "DeviceId = @DeviceId," +
                                 "YearUsingSince = @YearUsingSince," +
                                 "InventoryNo = @InventoryNo," +
                                 "SerialNumber = @SerialNumber," +
                                 "Ip = @Ip," +
                                 "Room = @Room," +
                                 "OwnerRegion = @OwnerRegion," +
                                 "InUse = @InUse," +
                                 "DateEdit = GETDATE() WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@DeviceId", model.DeviceId),
                new SqlParameter("@YearUsingSince" , model.YearUsingSince),
                new SqlParameter("@InventoryNo", (object)model.InventoryNo ?? DBNull.Value),
                new SqlParameter("@SerialNumber",(object)model.SerialNumber ?? DBNull.Value),
                new SqlParameter("@Ip", (object)model.Ip ?? DBNull.Value),
                new SqlParameter("@Room", (object)model.Room ?? DBNull.Value),
                new SqlParameter("@Note", (object)model.Note ?? DBNull.Value),
                new SqlParameter("@OwnerRegion", model.OwnerRegion),
                new SqlParameter("@InUse", model.InUse )
            });

        }

        public void Delete(VTNetworkDevice model)
        {
            DatabaseUtility.Exec("DELETE FROM VTNetworkDevice WHERE Id = @Id",
                new List<SqlParameter>() { new SqlParameter("@Id", model.Id) });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Наименование устройства", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NetworkDevice.NameNetworkDevice") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Серийный номер", DisplayMemberBinding = new Binding() { Path = new PropertyPath("SerialNumber") } });

            // ниже общие поля для всех моделей VT
            gridView.Columns.Add(new GridViewColumn() { Header = "Год учёта", DisplayMemberBinding = new Binding() { Path = new PropertyPath("YearUsingSince") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Инвентарный №", DisplayMemberBinding = new Binding() { Path = new PropertyPath("InventoryNo") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "IP адрес", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Ip") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Кабинет", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Room") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Примечание", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Note") } });

            DataTemplate inUseTemplate = new DataTemplate();
            FrameworkElementFactory inUseFactory = new FrameworkElementFactory(typeof(CheckBox));
            inUseFactory.SetValue(CheckBox.IsEnabledProperty, false);
            inUseFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("InUse") });
            inUseTemplate.VisualTree = inUseFactory;
            gridView.Columns.Add(new GridViewColumn() { Header = "Используется", CellTemplate = inUseTemplate });

            return gridView;
        }
    }
}
