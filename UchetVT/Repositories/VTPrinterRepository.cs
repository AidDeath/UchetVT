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
    class VTPrinterRepository : IVtRepository<VTPrinter>
    {
        public ObservableCollection<VTPrinter> GetAll(int regionId)
        {
            ObservableCollection<VTPrinter> vtPrinters = new ObservableCollection<VTPrinter>();
            DataTable vtPrintersTable = (regionId != 25) ? DatabaseUtility.GetTable("SELECT * FROM v_GetPrinters WHERE OwnerRegion IN (" + regionId + ")") :
                DatabaseUtility.GetTable("SELECT * FROM v_GetPrinters");

            foreach (DataRow row in vtPrintersTable.Rows)
            {
                VTPrinter vtPrinter = new VTPrinter()
                {
                    Id = row.Field<int>("Id"),
                    PrinterId = row.Field<int>("PrinterId"),
                    Printer = new Printer()
                    {
                        Id = row.Field<int>("PrinterId"),
                        NamePrinter = row.Field<string>("NamePrinter"),
                        MonoColor = row.Field<bool>("MonoColor"),
                        MFU = row.Field<bool>("MFU"),
                        LaserJet = row.Field<bool>("LaserJet")
                    },
                    // ниже общие поля для всех моделей VT
                    YearUsingSince = row.Field<int>("YearUsingSince"),
                    InventoryNo = row.Field<string>("InventoryNo"),
                    SerialNumber = row.Field<string>("SerialNumber"),
                    Ip = row.Field<string>("Ip"),
                    Room = row.Field<string>("Room"),
                    Note = row.Field<string>("Note"),
                    OwnerRegion = row.Field<int>("OwnerRegion"),
                    InUse = row.Field<bool>("InUse")
                };

                vtPrinters.Add(vtPrinter);
            }

            return vtPrinters;
        }

        public void Set(VTPrinter model)
        {
            DatabaseUtility.Exec(
                "INSERT INTO VTPrinter (PrinterId, YearUsingSince, InventoryNo, SerialNumber, Ip, Room, Note, OwnerRegion, InUse, DateEdit) VALUES (" +
                "@PrinterId, @YearUsingSince, @InventoryNo, @SerialNumber, @Ip, @Room, @Note, @OwnerRegion, @InUse, GETDATE())",
                new List<SqlParameter>()
                {
                    new SqlParameter("@PrinterId", model.PrinterId),
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

        public void Update(VTPrinter model)
        {
            DatabaseUtility.Exec("UPDATE VTPrinter SET " +
                                 "PrinterId = @PrinterId," +
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
                new SqlParameter("@PrinterId", model.PrinterId),
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

        public void Delete(VTPrinter model)
        {
            DatabaseUtility.Exec("DELETE FROM VTPrinter WHERE Id = @Id",
                new List<SqlParameter>() { new SqlParameter("@Id", model.Id) });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Наименование принтера", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Printer.NamePrinter") } });

            DataTemplate inUseTemplate = new DataTemplate();
            FrameworkElementFactory inUseFactory = new FrameworkElementFactory(typeof(CheckBox));
            inUseFactory.SetValue(CheckBox.IsEnabledProperty, false);
            inUseFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("InUse") });
            inUseTemplate.VisualTree = inUseFactory;
            gridView.Columns.Add(new GridViewColumn() { Header = "Используется", CellTemplate = inUseTemplate });

            gridView.Columns.Add(new GridViewColumn() { Header = "Серийный номер", DisplayMemberBinding = new Binding() { Path = new PropertyPath("SerialNumber") } });

            gridView.Columns.Add(new GridViewColumn() { Header = "Год учёта", DisplayMemberBinding = new Binding() { Path = new PropertyPath("YearUsingSince") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Инвентарный №", DisplayMemberBinding = new Binding() { Path = new PropertyPath("InventoryNo") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "IP адрес", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Ip") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Кабинет", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Room") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Примечание", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Note") } });

            DataTemplate mfuTemplate = new DataTemplate();
            FrameworkElementFactory mfuFactory = new FrameworkElementFactory(typeof(CheckBox));
            mfuFactory.SetValue(CheckBox.IsEnabledProperty, false);
            mfuFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("Printer.MFU") });
            mfuTemplate.VisualTree = mfuFactory;

            // Чекбоксы с особенностями принтера
            DataTemplate laserJetTemplate = new DataTemplate();
            FrameworkElementFactory laserJetFactory = new FrameworkElementFactory(typeof(CheckBox));
            laserJetFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("Printer.LaserJet") });
            laserJetFactory.SetValue(CheckBox.IsEnabledProperty, false);
            laserJetTemplate.VisualTree = laserJetFactory;

            DataTemplate monoColorTemplate = new DataTemplate();
            FrameworkElementFactory monoColorFactory = new FrameworkElementFactory(typeof(CheckBox));
            monoColorFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("Printer.MonoColor") });
            monoColorFactory.SetValue(CheckBox.IsEnabledProperty, false);
            monoColorTemplate.VisualTree = monoColorFactory;

            gridView.Columns.Add(new GridViewColumn() { Header = "МФУ", CellTemplate = mfuTemplate });
            gridView.Columns.Add(new GridViewColumn() { Header = "Лазерный", CellTemplate = laserJetTemplate });
            gridView.Columns.Add(new GridViewColumn() { Header = "Монохромный", CellTemplate = monoColorTemplate });

            return gridView;
        }
    }
}
