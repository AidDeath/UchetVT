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
    class VTComputerRepository :IVtRepository<VTComputer>
    {
        public ObservableCollection<VTComputer> GetAll(int regionId)
        {
            ObservableCollection<VTComputer> vtComputers = new ObservableCollection<VTComputer>();
            DataTable computersTable = (regionId != 25)? 
                DatabaseUtility.GetTable("SELECT * FROM v_GetComputers WHERE OwnerRegion IN ("+regionId+")") : 
                DatabaseUtility.GetTable("SELECT * FROM v_GetComputers");

            foreach (DataRow row in computersTable.Rows)
            {
                VTComputer vtComputer = new VTComputer
                {
                    Id = row.Field<int>("Id"),
                    BoardId = row.Field<int>("BoardId"),
                    Board = new Board()
                    { Id = row.Field<int>("BoardId"), Motherboard = row.Field<string>("Motherboard") },
                    CpuId = row.Field<int>("CPUId"),
                    Cpu = new CPU() { Id = row.Field<int>("BoardId"), NameCPU = row.Field<string>("NameCPU") },
                    CpuClockSpeed = row.Field<int>("CpuClockSpeed"),
                    HddId = row.Field<int>("HddId"),
                    Hdd = new HDD() { Id = row.Field<int>("HddId"), NameHDD = row.Field<string>("NameHDD") },
                    HddCapacity = row.Field<int>("HddCapacity"),
                    RamCapacity = row.Field<double>("RamCapacity"),
                    YearUsingSince = row.Field<int>("YearUsingSince"),
                    InventoryNo = row.Field<string>("InventoryNo"),
                    Hostname = row.Field<string>("HostName"),
                    IpAddress = row.Field<string>("Ip"),
                    WorkerName = row.Field<string>("WorkerName"),
                    Room = row.Field<string>("Room"),
                    OsId = row.Field<int>("OSId"),
                    OS = new OS() { Id = row.Field<int>("OSId"), NameOS = row.Field<string>("NameOS") },
                    LicenseId = row.Field<int>("LicenseStateId"),
                    License = new License()
                    { Id = row.Field<int>("LicenseStateId"), LicenseState = row.Field<string>("License") },
                    Note = row.Field<string>("Note"),
                    InUse = row.Field<bool>("InUse")
                };

                vtComputers.Add(vtComputer);
            }

            return vtComputers;
        }

        public VTComputer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(VTComputer model)
        {                                                         
            //MessageBox.Show(model.OwnerRegion.ToString());
            DatabaseUtility.Exec("INSERT INTO VTComputer (InUse, BoardId, CPUId, CPUClockSpeed, HddId, HddCapacity, RamCapacity, YearUsingSince," +
                                 "InventoryNo, HostName, Ip, WorkerName, Room, Note, OwnerRegion, DateEdit, OSId, LicenseStateId) VALUES (" +
                                 "@InUse, @BoardId, @CPUId, @CPUClockSpeed, @HddId, @HddCapacity, @RamCapacity, @YearUsingSince," +
                                 "@InventoryNo, @HostName, @Ip, @WorkerName, @Room, @Note, @OwnerRegion, GETDATE(), @OSId, @LicenseStateId )",
                new List<SqlParameter>()
                {
                    new SqlParameter("@InUse", model.InUse),
                    new SqlParameter("@BoardId", model.BoardId),
                    new SqlParameter("@CPUId", model.CpuId),
                    new SqlParameter("@CPUClockSpeed", model.CpuClockSpeed),
                    new SqlParameter("@HddId", model.HddId),
                    new SqlParameter("@HddCapacity", model.HddCapacity),
                    new SqlParameter("@RamCapacity", model.RamCapacity),
                    new SqlParameter("@YearUsingSince", model.YearUsingSince),
                    new SqlParameter("@InventoryNo", (object)model.InventoryNo ?? DBNull.Value),
                    new SqlParameter("@HostName", (object)model.Hostname ?? DBNull.Value),
                    new SqlParameter("@Ip", (object)model.IpAddress ?? DBNull.Value),
                    new SqlParameter("@WorkerName", (object)model.WorkerName ?? DBNull.Value),
                    new SqlParameter("@Room", (object)model.Room ?? DBNull.Value),
                    new SqlParameter("@Note", (object)model.Note ?? DBNull.Value),
                    new SqlParameter("@OwnerRegion", model.OwnerRegion),
                    new SqlParameter("@OSId", model.OsId),
                    new SqlParameter("@LicenseStateId", model.LicenseId)

                });

        }

        public void Update(VTComputer model)
        {
            DatabaseUtility.Exec(
                "UPDATE VTComputer SET " +
                "InUse = @InUse," +
                "BoardId = @BoardId," +
                "CPUId = @CPUId, " +
                "CPUClockSpeed = @CPUClockSpeed, " +
                "HddId = @HddId, " +
                "HddCapacity = @HddCapacity, " +
                "RamCapacity = @RamCapacity, " +
                "YearUsingSince = @YearUsingSince," +
                "InventoryNo = @InventoryNo," +
                "HostName = @HostName, " +
                "Ip = @Ip, " +
                "WorkerName = @WorkerName, " +
                "Room = @Room, " +
                "Note = @Note," +
                "OwnerRegion = @OwnerRegion," +
                "DateEdit = GETDATE(), " +
                "OSId = @OSId, " +
                "LicenseStateId = @LicenseStateId WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@InUse", model.InUse),
                    new SqlParameter("@BoardId", model.BoardId),
                    new SqlParameter("@CPUId", model.CpuId),
                    new SqlParameter("@CPUClockSpeed", model.CpuClockSpeed),
                    new SqlParameter("@HddId", model.HddId),
                    new SqlParameter("@HddCapacity", model.HddCapacity),
                    new SqlParameter("@RamCapacity", model.RamCapacity),
                    new SqlParameter("@YearUsingSince", model.YearUsingSince),
                    new SqlParameter("@InventoryNo", (object)model.InventoryNo ?? DBNull.Value),
                    new SqlParameter("@HostName", (object)model.Hostname ?? DBNull.Value),
                    new SqlParameter("@Ip", (object)model.IpAddress ?? DBNull.Value),
                    new SqlParameter("@WorkerName", (object)model.WorkerName ?? DBNull.Value),
                    new SqlParameter("@Room", (object)model.Room ?? DBNull.Value),
                    new SqlParameter("@Note", (object)model.Note ?? DBNull.Value),
                    new SqlParameter("@OwnerRegion", model.OwnerRegion),
                    new SqlParameter("@OSId", model.OsId),
                    new SqlParameter("@LicenseStateId", model.LicenseId)
                });
        }

        public void Delete(VTComputer model)
        {
            DatabaseUtility.Exec("DELETE FROM VTComputer WHERE Id = @Id",
                new List<SqlParameter>() {new SqlParameter("@Id", model.Id)});
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Материнская плата", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Board.Motherboard") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "ЦП", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Cpu.NameCPU") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Частота ядра", DisplayMemberBinding = new Binding() { Path = new PropertyPath("CpuClockSpeed") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Жесткий диск", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Hdd.NameHDD") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Объем", DisplayMemberBinding = new Binding() { Path = new PropertyPath("HddCapacity") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "ОЗУ", DisplayMemberBinding = new Binding() { Path = new PropertyPath("RamCapacity") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Операционная система", DisplayMemberBinding = new Binding() { Path = new PropertyPath("OS.NameOS") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Год учёта", DisplayMemberBinding = new Binding() { Path = new PropertyPath("YearUsingSince") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Инвентарный №", DisplayMemberBinding = new Binding() { Path = new PropertyPath("InventoryNo") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Имя в сети", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Hostname") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "IP адрес", DisplayMemberBinding = new Binding() { Path = new PropertyPath("IpAddress") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Сотрудник", DisplayMemberBinding = new Binding() { Path = new PropertyPath("WorkerName") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Кабинет", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Room") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Лицензия", DisplayMemberBinding = new Binding() { Path = new PropertyPath("License.LicenseState") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Примечание", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Note") } });

            DataTemplate inUseTemplate = new DataTemplate();
            FrameworkElementFactory inUseFactory = new FrameworkElementFactory(typeof(CheckBox));
            inUseFactory.SetValue(CheckBox.IsEnabledProperty, false);
            inUseFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("InUse") });
            inUseTemplate.VisualTree = inUseFactory;
            gridView.Columns.Add(new GridViewColumn() { Header = "Используется", CellTemplate = inUseTemplate});



            return gridView;
        }
    }
}
