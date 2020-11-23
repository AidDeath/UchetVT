using System;
using System.Data;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace UchetVT
{
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUnitOfWork DB;
        
        public User CurrentUser;

        public enum SideView
        {
            BookList,
            RegionList
        }
        public enum MainView
        {
            Computer,
            Printer,
            UPS,
            Network
        }

        public SideView CurrentSideView;
        public MainView CurrentMainView;

        public MainWindow()
        {
            InitializeComponent();
            DB = new UnitOfWork();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentUser.AccessToBook)) this.NRIButton.Visibility = Visibility.Hidden;
                else System.Windows.Application.Current.Properties.Add("AccessToBook", CurrentUser.AccessToBook);

            System.Windows.Application.Current.Properties.Add("AccessToRegion", CurrentUser.AccessToRegion);

            this.Title += " -- " + CurrentUser.Name;
        }


        private void NRIButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Список справочников
            SideListView.View = DB.Books.MakeView();
            SideListView.ItemsSource = DB.Books.GetAll();
            MainListView.View = new GridView();
            MainListView.ItemsSource = null;
            CurrentSideView = SideView.BookList;
        }
 
        private void SideListView_OnMouseUp(object sender, RoutedEventArgs e)
        {
            switch (CurrentSideView)
            {
                case SideView.BookList:
                    {                           //Выбор справочника в боковом списке
                        Book b = (Book)SideListView.SelectedItem;
                        if (b is Book)
                        {
                            switch (b.Id)
                            {
                                case 1:
                                {
                                    MainListView.View = DB.Boards.MakeView();
                                    MainListView.ItemsSource = DB.Boards.GetAll();
                                    break;
                                }

                                case 2:
                                {
                                    MainListView.View = DB.CPUs.MakeView();
                                    MainListView.ItemsSource = DB.CPUs.GetAll();
                                    break;
                                }

                                case 3:
                                {
                                    MainListView.View = DB.HDDs.MakeView();
                                    MainListView.ItemsSource = DB.HDDs.GetAll();
                                    break;
                                }

                                case 4:
                                {
                                    MainListView.View = DB.Licenses.MakeView();
                                    MainListView.ItemsSource = DB.Licenses.GetAll();
                                    break;
                                }

                                case 5:
                                {
                                    MainListView.View = DB.NetworkDevices.MakeView();
                                    MainListView.ItemsSource = DB.NetworkDevices.GetAll();
                                    break;
                                }

                                case 6:
                                {
                                    MainListView.View = DB.OSes.MakeView();
                                    MainListView.ItemsSource = DB.OSes.GetAll();
                                    break;
                                }

                                case 7:
                                {
                                    MainListView.View = DB.Printers.MakeView();
                                    MainListView.ItemsSource = DB.Printers.GetAll();
                                    break;
                                }

                                case 8:
                                {
                                    MainListView.View = DB.Regions.MakeView();
                                    MainListView.ItemsSource = DB.Regions.GetAll();
                                    break;
                                }

                                case 9:
                                {
                                    MainListView.View = DB.UPSes.MakeView();
                                    MainListView.ItemsSource = DB.UPSes.GetAll();
                                    break;
                                }

                                case 10:
                                {
                                    MainListView.View = DB.Users.MakeView();
                                    MainListView.ItemsSource = DB.Users.GetAll();
                                    break;
                                }


                            }
                        }
                        break;
                    }

                case SideView.RegionList:
                {                               //Выбор района в боковом списке
                    Region r = (Region) SideListView.SelectedItem;
                    if (r is Region)
                    {
                        switch (CurrentMainView)
                        {
                            case MainView.Computer:
                            {
                                MainListView.View = DB.VTComputers.MakeView();
                                MainListView.ItemsSource = DB.VTComputers.GetAll(r.Id);
                                break;
                            }

                            case MainView.Printer:
                            {
                                MainListView.View = DB.VTPrinters.MakeView();
                                MainListView.ItemsSource = DB.VTPrinters.GetAll(r.Id);
                                break;
                            }

                            case MainView.Network:
                            {
                                MainListView.View = DB.VTNetworkDevices.MakeView();
                                MainListView.ItemsSource = DB.VTNetworkDevices.GetAll(r.Id);
                                break;
                            }

                            case MainView.UPS:
                            {
                                MainListView.View = DB.VTUPSes.MakeView();
                                MainListView.ItemsSource = DB.VTUPSes.GetAll(r.Id);
                                break;
                            }

                            }

                        
                    }
                    break;
                }
            }
        }
        
        private void CompButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMainView = MainView.Computer;
            MainViewPrepare();
        }

        private void PrinterButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMainView = MainView.Printer;
            MainViewPrepare();
        }

        private void UPSButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMainView = MainView.UPS;
            MainViewPrepare();
        }

        private void NetworkDeviceButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMainView = MainView.Network;
            MainViewPrepare();
        }

        private void MainViewPrepare()   // Подготовка главной области окна к выводу информации о технике
        {
            SideListView.View = DB.Regions.MakeView();
            SideListView.ItemsSource = DB.Regions.GetAll();
            MainListView.View = new GridView();
            CurrentSideView = SideView.RegionList;
            MainListView.ItemsSource = null;
        }
        
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {

            switch (this.MainListView.SelectedItem.GetType().ToString())
            {
                case "UchetVT.Board":
                {
                    DB.Boards.Delete((Board) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.Boards.GetAll();
                    break;
                }

                case "UchetVT.CPU":
                {
                    DB.CPUs.Delete((CPU) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.CPUs.GetAll();
                    break;
                }

                case "UchetVT.HDD":
                {
                    DB.HDDs.Delete((HDD) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.HDDs.GetAll();
                    break;
                }

                case "UchetVT.License":
                {
                    DB.Licenses.Delete((License) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.Licenses.GetAll();
                    break;
                }

                case "UchetVT.NetworkDevice":
                {
                    DB.NetworkDevices.Delete((NetworkDevice) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.NetworkDevices.GetAll();
                    break;
                }

                case "UchetVT.OS":
                {
                    DB.OSes.Delete((OS) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.OSes.GetAll();
                    break;
                }

                case "UchetVT.Printer":
                {
                    DB.Printers.Delete((Printer) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.Printers.GetAll();
                    break;
                }

                case "UchetVT.Region":
                {
                    //DB.Regions.Delete((Region)MainListView.SelectedItem);
                    //MainListView.ItemsSource = DB.Regions.GetAll();
                    MessageBox.Show("Удаление районов не допускается!");
                    break;
                }

                case "UchetVT.UPS":
                {
                    DB.UPSes.Delete((UPS) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.UPSes.GetAll();
                    break;
                }

                case "UchetVT.User":
                {
                    DB.Users.Delete((User)MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.Users.GetAll();
                    break;
                }

                case "UchetVT.VTComputer":
                {
                    DB.VTComputers.Delete((VTComputer) MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.VTComputers.GetAll(((Region) SideListView.SelectedItem).Id);
                    break;
                }

                case "UchetVT.VTNetworkDevice":
                {
                    DB.VTNetworkDevices.Delete((VTNetworkDevice)MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.VTNetworkDevices.GetAll(((Region)SideListView.SelectedItem).Id);
                    break;
                }

                case "UchetVT.VTPrinter":
                {
                    DB.VTPrinters.Delete((VTPrinter)MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.VTPrinters.GetAll(((Region)SideListView.SelectedItem).Id);
                    break;
                }

                case "UchetVT.VTUPS":
                {
                    DB.VTUPSes.Delete((VTUPS)MainListView.SelectedItem);
                    MainListView.ItemsSource = DB.VTUPSes.GetAll(((Region)SideListView.SelectedItem).Id);
                    break;
                }

                default:
                {
                    MessageBox.Show("Не выбрана запись");
                    break;
                }

            }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainListView.SelectedItem = null;
            EditButton_OnClick(sender, e);
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SideListView.SelectedItem == null)
            {
                MessageBox.Show("Не выбран район или справочник");
                return;
            }
            RecordWindow recordWindow = new RecordWindow { Owner = this };
            //addRecordWindow.ShowDialog()
            //if (addRecordWindow.ShowDialog() == true) MessageBox.Show("OK!");

            switch (CurrentSideView)
            {
                case SideView.BookList:
                    {
                        Book b = (Book)SideListView.SelectedItem;
                        if (b is Book)
                        {
                            switch (b.Id)
                            {
                                case 1:
                                    {
                                        if (MainListView.SelectedItem is Board)
                                            recordWindow.DataContext = MainListView.SelectedItem;
                                        else
                                        {
                                            Board board = new Board() { Motherboard = "", YearOut = null };
                                            recordWindow.DataContext = board;
                                        }
                                        recordWindow.CurrType = typeof(Board);
                                        recordWindow.GroupBox.Content = TryFindResource("BoardFieldSet");


                                        if (recordWindow.ShowDialog() == true)
                                        {
                                            MainListView.ItemsSource = DB.Boards.GetAll();
                                            MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                        }
                                        break;
                                    }

                                case 2:
                                    {
                                        if (MainListView.SelectedItem is CPU)
                                            recordWindow.DataContext = MainListView.SelectedItem;
                                        else
                                        {
                                            CPU cpu = new CPU() { NameCPU = ""};
                                            recordWindow.DataContext = cpu;
                                        }
                                        recordWindow.CurrType = typeof(CPU);
                                        recordWindow.GroupBox.Content = TryFindResource("CPUFieldSet");

                                        if (recordWindow.ShowDialog() == true)
                                        {
                                            MainListView.ItemsSource = DB.CPUs.GetAll();
                                            MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        if (MainListView.SelectedItem is HDD)
                                            recordWindow.DataContext = MainListView.SelectedItem;
                                        else
                                        {
                                            HDD hdd = new HDD() { NameHDD = "" };
                                            recordWindow.DataContext = hdd;
                                        }
                                        recordWindow.CurrType = typeof(HDD);
                                        recordWindow.GroupBox.Content = TryFindResource("HDDFieldSet");

                                        if (recordWindow.ShowDialog() == true)
                                        {
                                            MainListView.ItemsSource = DB.HDDs.GetAll();
                                            MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        if (MainListView.SelectedItem is License)
                                            recordWindow.DataContext = MainListView.SelectedItem;
                                        else
                                        {
                                            License license = new License() { LicenseState = "" };
                                            recordWindow.DataContext = license;
                                        }
                                        recordWindow.CurrType = typeof(License);
                                        recordWindow.GroupBox.Content = TryFindResource("LicenseFieldSet");

                                        if (recordWindow.ShowDialog() == true)
                                        {
                                            MainListView.ItemsSource = DB.Licenses.GetAll();
                                            MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                        }
                                        break;
                                    }
                                case 5:
                                    {
                                        if (MainListView.SelectedItem is NetworkDevice)
                                            recordWindow.DataContext = MainListView.SelectedItem;
                                        else
                                        {
                                            NetworkDevice networkDevice = new NetworkDevice() { NameNetworkDevice = "" };
                                            recordWindow.DataContext = networkDevice;
                                        }
                                        recordWindow.CurrType = typeof(NetworkDevice);
                                        recordWindow.GroupBox.Content = TryFindResource("NetworkDeviceFieldSet");

                                        if (recordWindow.ShowDialog() == true)
                                        {
                                            MainListView.ItemsSource = DB.NetworkDevices.GetAll();
                                            MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                        }
                                        break;
                                    }
                                case 6:
                                {
                                    if (MainListView.SelectedItem is OS)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        OS os = new OS() { NameOS = "" };
                                        recordWindow.DataContext = os;
                                    }
                                    recordWindow.CurrType = typeof(OS);
                                    recordWindow.GroupBox.Content = TryFindResource("OSFieldSet");


                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        MainListView.ItemsSource = DB.OSes.GetAll();
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                    }
                                case 7:
                                {
                                    if (MainListView.SelectedItem is Printer)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        Printer printer = new Printer() { NamePrinter = "" , LaserJet = false, MFU = false, MonoColor = false};
                                        recordWindow.DataContext = printer;
                                    }
                                    recordWindow.CurrType = typeof(Printer);
                                    recordWindow.GroupBox.Content = TryFindResource("PrinterFieldSet");

                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        MainListView.ItemsSource = DB.Printers.GetAll();
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                    }
                                case 8:
                                {
                                    //регионы
                                    break;
                                }                                
                                case 9:
                                {
                                    if (MainListView.SelectedItem is UPS)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        UPS ups = new UPS() { NameUPS = "" };
                                        recordWindow.DataContext = ups;
                                    }
                                    recordWindow.CurrType = typeof(UPS);
                                    recordWindow.GroupBox.Content = TryFindResource("UPSFieldSet");

                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        MainListView.ItemsSource = DB.UPSes.GetAll();
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                    }                                
                                case 10:
                                {
                                    if (MainListView.SelectedItem is User)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        User user = new User() { Name = "", Position = "", UserName = "", AccessToRegion = "", AccessToBook = ""};
                                        recordWindow.DataContext = user;
                                    }
                                    recordWindow.CurrType = typeof(User);
                                    recordWindow.GroupBox.Content = TryFindResource("UserFieldSet");

                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        MainListView.ItemsSource = DB.Users.GetAll();
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                    }
                            }

                        }
                        break;
                    }

                case SideView.RegionList:
                    {
                        switch (CurrentMainView)
                        {
                            case MainView.Computer:
                                {
                                    if (MainListView.SelectedItem is VTComputer)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        VTComputer vtComputer = new VTComputer()
                                        {
                                            Board = new Board(),
                                            Cpu = new CPU(),
                                            Hdd = new HDD(),
                                            License = new License(),
                                            OS = new OS()
                                        };
                                        recordWindow.DataContext = vtComputer;
                                    }
                                    recordWindow.CurrType = typeof(VTComputer);
                                    recordWindow.GroupBox.Content = TryFindResource("VTComputerFieldSet");


                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        Region r = (Region)SideListView.SelectedItem;
                                        MainListView.ItemsSource = DB.VTComputers.GetAll(r.Id);
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                }
                            case MainView.Printer:
                                {
                                    if (MainListView.SelectedItem is VTPrinter)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        VTPrinter vtPrinter = new VTPrinter()
                                        {
                                            Printer = new Printer()
                                        };
                                        recordWindow.DataContext = vtPrinter;
                                    }
                                    recordWindow.CurrType = typeof(VTPrinter);
                                    recordWindow.GroupBox.Content = TryFindResource("VTPrinterFieldSet");


                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        Region r = (Region)SideListView.SelectedItem;
                                        MainListView.ItemsSource = DB.VTPrinters.GetAll(r.Id);
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                }
                            case MainView.Network:
                                {
                                    if (MainListView.SelectedItem is VTNetworkDevice)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        VTNetworkDevice vtNetworkDevice = new VTNetworkDevice()
                                        {
                                            NetworkDevice = new NetworkDevice()
                                        };
                                        recordWindow.DataContext = vtNetworkDevice;
                                    }
                                    recordWindow.CurrType = typeof(VTNetworkDevice);
                                    recordWindow.GroupBox.Content = TryFindResource("VTNetworkDeviceFieldSet");


                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        Region r = (Region)SideListView.SelectedItem;
                                        MainListView.ItemsSource = DB.VTNetworkDevices.GetAll(r.Id);
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                }
                            case MainView.UPS:
                                {
                                    if (MainListView.SelectedItem is VTUPS)
                                        recordWindow.DataContext = MainListView.SelectedItem;
                                    else
                                    {
                                        VTUPS vtNetworkDevice = new VTUPS()
                                        {
                                            UPS = new UPS()
                                        };
                                        recordWindow.DataContext = vtNetworkDevice;
                                    }
                                    recordWindow.CurrType = typeof(VTUPS);
                                    recordWindow.GroupBox.Content = TryFindResource("VTUPSFieldSet");


                                    if (recordWindow.ShowDialog() == true)
                                    {
                                        Region r = (Region)SideListView.SelectedItem;
                                        MainListView.ItemsSource = DB.VTUPSes.GetAll(r.Id);
                                        MainListView.SelectedIndex = MainListView.Items.Count - 1;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }

        }

        private void MainListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditButton_OnClick(sender, e);
        }
    }

}

