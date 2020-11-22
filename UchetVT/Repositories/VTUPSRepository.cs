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
    class VTUPSRepository :IVtRepository<VTUPS>
    {
        public ObservableCollection<VTUPS> GetAll(int regionId)
        {
            ObservableCollection<VTUPS> vtUPSes = new ObservableCollection<VTUPS>();
            DataTable vtUPSTable = (regionId != 25) ? DatabaseUtility.GetTable("SELECT * FROM v_GetUPSes WHERE OwnerRegion IN (" + regionId + ")") :
                DatabaseUtility.GetTable("SELECT * FROM v_GetUPSes");

            foreach (DataRow row in vtUPSTable.Rows)
            {
                VTUPS vtUPS = new VTUPS()
                {
                    Id = row.Field<int>("Id"),
                    UPSId = row.Field<int>("UPSId"),
                    UPS = new UPS()
                    {
                        Id = row.Field<int>("UPSId"),
                        NameUPS = row.Field<string>("NameUPS")
                    },
                    // ниже общие поля для всех моделей VT
                    YearUsingSince = row.Field<int>("YearUsingSince"),
                    InventoryNo = row.Field<string>("InventoryNo"),
                    SerialNumber = row.Field<string>("SerialNumber"),
                    Room = row.Field<string>("Room"),
                    Note = row.Field<string>("Note"),
                    OwnerRegion = row.Field<int>("OwnerRegion"),
                    InUse = row.Field<bool>("InUse")
                };

                vtUPSes.Add(vtUPS);
            }

            return vtUPSes;
        }

        public void Set(VTUPS model)
        {
            DatabaseUtility.Exec(
                "INSERT INTO VTUPS (UPSId, YearUsingSince, InventoryNo, SerialNumber, Room, Note, OwnerRegion, InUse, DateEdit) VALUES (" +
                "@UPSId, @YearUsingSince, @InventoryNo, @SerialNumber, @Room, @Note, @OwnerRegion, @InUse, GETDATE())",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UPSId", model.UPSId),
                    new SqlParameter("@YearUsingSince", model.YearUsingSince),
                    new SqlParameter("@InventoryNo", (object)model.InventoryNo ?? DBNull.Value),
                    new SqlParameter("@SerialNumber",(object)model.SerialNumber ?? DBNull.Value),
                    new SqlParameter("@Room", (object)model.Room ?? DBNull.Value),
                    new SqlParameter("@Note", (object)model.Note ?? DBNull.Value),
                    new SqlParameter("@OwnerRegion", model.OwnerRegion),
                    new SqlParameter("@InUse", model.InUse)
                });
        }

        public void Update(VTUPS model)
        {
            DatabaseUtility.Exec("UPDATE VTUPS SET " +
                                 "UPSId = @UPSId," +
                                 "YearUsingSince = @YearUsingSince," +
                                 "InventoryNo = @InventoryNo," +
                                 "SerialNumber = @SerialNumber," +
                                 "Room = @Room," +
                                 "Note = @Note," +
                                 "OwnerRegion = @OwnerRegion," +
                                 "InUse = @InUse," +
                                 "DateEdit = GETDATE() WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@UPSId", model.UPSId),
                new SqlParameter("@YearUsingSince" , model.YearUsingSince),
                new SqlParameter("@InventoryNo", (object)model.InventoryNo ?? DBNull.Value),
                new SqlParameter("@SerialNumber", (object)model.SerialNumber ?? DBNull.Value),
                new SqlParameter("@Room", (object)model.Room ?? DBNull.Value),
                new SqlParameter("@Note", (object)model.Note ?? DBNull.Value),
                new SqlParameter("@OwnerRegion", model.OwnerRegion),
                new SqlParameter("@InUse", model.InUse )
            });
        }

        public void Delete(VTUPS model)
        {
            DatabaseUtility.Exec("DELETE FROM VTUPS WHERE Id = @Id",
                new List<SqlParameter>() { new SqlParameter("@Id", model.Id) });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Наименование ИБП", DisplayMemberBinding = new Binding() { Path = new PropertyPath("UPS.NameUPS") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Серийный номер", DisplayMemberBinding = new Binding() { Path = new PropertyPath("SerialNumber") } });

            // ниже общие поля для всех моделей VT
            gridView.Columns.Add(new GridViewColumn() { Header = "Год учёта", DisplayMemberBinding = new Binding() { Path = new PropertyPath("YearUsingSince") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Инвентарный №", DisplayMemberBinding = new Binding() { Path = new PropertyPath("InventoryNo") } });
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
