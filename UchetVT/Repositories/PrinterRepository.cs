using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UchetVT
{
    class PrinterRepository : IRepository<Printer>
    {
        public ObservableCollection<Printer> GetAll()
        {
            ObservableCollection<Printer> printers = new ObservableCollection<Printer>();
            DataTable printerTable = DatabaseUtility.GetTable("SELECT * FROM BookPrinter");
            foreach (DataRow row in printerTable.Rows)
            {
                printers.Add(new Printer()
                {
                    Id = row.Field<int>("Id"),
                    NamePrinter = row.Field<string>("NamePrinter"),
                    MFU = row.Field<bool>("MFU"),
                    LaserJet = row.Field<bool>("LaserJet"),
                    MonoColor = row.Field<bool>("MonoColor")
                });
            }

            return printers;
        }


        public Printer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(Printer model)
        {
            DatabaseUtility.Exec("INSERT INTO BookPrinter ( NamePrinter, LaserJet, MFU, MonoColor) VALUES (@NamePrinter, @LaserJet, @MFU, @MonoColor)", new List<SqlParameter>()
            {
                new SqlParameter("@NamePrinter", model.NamePrinter),
                new SqlParameter("@LaserJet", model.LaserJet),
                new SqlParameter("@MFU", model.MFU),
                new SqlParameter("@MonoColor", model.MonoColor)
            });
        }

        public void Update(Printer model)
        {
            DatabaseUtility.Exec("UPDATE BookPrinter SET  NamePrinter = @NamePrinter , LaserJet = @LaserJet, MFU = @MFU, MonoColor = @MonoColor" +
                                 " WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@NamePrinter", model.NamePrinter),
                new SqlParameter("@LaserJet", model.LaserJet),
                new SqlParameter("@MFU", model.MFU),
                new SqlParameter("@MonoColor", model.MonoColor)
            });
        }

        public void Delete(Printer model)
        {
            DatabaseUtility.Exec("DELETE FROM BookPrinter WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Модель принтера", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NamePrinter") } });


            //TODO : Remove test string 
            //gridView.Columns.Add(new GridViewColumn(){ Header = "TEST", CellTemplate = new DataTemplate(typeof(CheckBox)),DisplayMemberBinding = new Binding(){Path = new PropertyPath("MFU")}});
            //Grid chkgrid = new Grid();
            //chkgrid.Children.Add(new CheckBox());

            DataTemplate mfuTemplate = new DataTemplate();
            FrameworkElementFactory mfuFactory = new FrameworkElementFactory(typeof(CheckBox));
            mfuFactory.SetValue(CheckBox.IsEnabledProperty, false);
            mfuFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding(){Path = new PropertyPath("MFU")});
            mfuTemplate.VisualTree = mfuFactory;

            DataTemplate laserJetTemplate = new DataTemplate();
            FrameworkElementFactory laserJetFactory = new FrameworkElementFactory(typeof(CheckBox));
            laserJetFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("LaserJet") });
            laserJetFactory.SetValue(CheckBox.IsEnabledProperty, false);
            laserJetTemplate.VisualTree = laserJetFactory;

            DataTemplate monoColorTemplate = new DataTemplate();
            FrameworkElementFactory monoColorFactory = new FrameworkElementFactory(typeof(CheckBox));
            monoColorFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("MonoColor") });
            monoColorFactory.SetValue(CheckBox.IsEnabledProperty, false);
            monoColorTemplate.VisualTree = monoColorFactory;

            //factory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);

            //gridView.Columns.Add(new GridViewColumn(){ Header = "TEST" , DisplayMemberBinding = new Binding() { Path = new PropertyPath("MFU") }, CellTemplate = template });
            //gridView.Columns.Add(new GridViewColumn() { Header = "TEST", DisplayMemberBinding = new Binding() { Path = new PropertyPath("MFU") }, CellTemplate = template });
            //gridView.Columns.Add(new GridViewColumn() { Header = "TEST", DisplayMemberBinding = new Binding() { Path = new PropertyPath("MFU") }, CellTemplate = template });

            gridView.Columns.Add(new GridViewColumn() { Header = "МФУ", CellTemplate = mfuTemplate});
            gridView.Columns.Add(new GridViewColumn() { Header = "Лазерный", CellTemplate = laserJetTemplate });
            gridView.Columns.Add(new GridViewColumn() { Header = "Монохромный", CellTemplate = monoColorTemplate });

            return gridView;
        }
    }
}
