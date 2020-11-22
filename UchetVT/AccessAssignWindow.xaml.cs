using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UchetVT
{
    /// <summary>
    /// Interaction logic for AccessAssignWindow.xaml
    /// </summary>
    public partial class AccessAssignWindow : Window
    {
        public enum WorkWithVariants {Region, Book}
        public WorkWithVariants WorkWith;

        public string AccessString = "";  // TODO: НЕ ПЕРЕДАЁТСЯ, СКОТИНА!!! 

       
        

        public AccessAssignWindow()
        {
            InitializeComponent();

            //var array = AccessString.Split(new char[]{','});



            if (WorkWith == WorkWithVariants.Region)
            {

                var gridView = new GridView();
                gridView.Columns.Add(new GridViewColumn
                    {Header = "Ид", DisplayMemberBinding = new Binding() {Path = new PropertyPath("Id")}});
                gridView.Columns.Add(new GridViewColumn
                    {Header = "Город", DisplayMemberBinding = new Binding() {Path = new PropertyPath("NameCity")}});
                gridView.Columns.Add(new GridViewColumn
                    { Header = "Доступ", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Access") } });

               // new AccessRegionList(array);

                AccessControlListView.View = gridView;
               // AccessControlListView.ItemsSource = AccessRegionList.List;

            }


        }
    }

    internal class AccessRegionList
    {
        public int Id { get; set; }
        public string NameCity { get; set; }
        public bool Access { get; set; }

        public static ObservableCollection<AccessRegionList> List;

        public AccessRegionList() { }
        public AccessRegionList(string[] regionlist)
        {
            IUnitOfWork DB = new UnitOfWork();
            var regions = DB.Regions.GetAll();

            foreach (var region in regions)
            {
                List.Add(new AccessRegionList()
                {
                    Id = region.Id,
                    NameCity = region.NameCity,
                    Access = (regionlist.Any(r => r == region.Id.ToString())) ? true : false
                });
            }

        }

        

    }
}


