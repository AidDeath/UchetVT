using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UchetVT
{
    /// <summary>
    /// Interaction logic for AddRecordWindow.xaml
    /// </summary>
    public partial class RecordWindow : Window
    {
        public ICommand SaveChangesCommand { get; }

        private bool CanDetailsBoardCommandExecute(object param)
        {
            var id = (int?)param;

            switch (id)
            {
                case null:
                case 0:
                    return false;
                default:
                    return true;
            }
        }

        private void OnDetailsBoardCommandExecuted(object param)
        {

        }

        // TODO: доделать валидацию данных и отключение кнопки ОК по привязке

        public Type CurrType;
        private IUnitOfWork DB;

        public bool IsRecordOK { get; set; }



        public RecordWindow()
        {
            InitializeComponent();
        //  this.AddChild((StackPanel)this.FindResource("LicenseFieldSet"));
          //this.AddLogicalChild((StackPanel)this.FindResource("LicenseFieldSet"));
          //this.FieldSet.Children.Add();
          DB = new UnitOfWork();
          IsRecordOK = false;

        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            switch (CurrType.ToString())
            {
                case "UchetVT.Board":
                {
                    Board board = (Board)this.DataContext;

                    if (board.HasError)
                    {
                        MessageBox.Show("");
                        break;
                    }

                    if (board.Id == 0) DB.Boards.Set(board);
                    else DB.Boards.Update(board);
                    
                    break;
                }  
                
                case "UchetVT.CPU":
                {
                    CPU cpu = (CPU)this.DataContext;

                    if (cpu.Id == 0) DB.CPUs.Set(cpu);
                    else DB.CPUs.Update(cpu);
                    
                    break;
                }
                case "UchetVT.HDD":
                {
                    HDD hdd = (HDD)this.DataContext;

                    if (hdd.Id == 0) DB.HDDs.Set(hdd);
                    else DB.HDDs.Update(hdd);
                    
                    break;
                }
                case "UchetVT.License":
                {
                    License license = (License)this.DataContext;

                    if (license.Id == 0) DB.Licenses.Set(license);
                    else DB.Licenses.Update(license);
                    
                    break;
                }
                case "UchetVT.NetworkDevice":
                {
                    NetworkDevice networkDevice = (NetworkDevice)this.DataContext;

                    if (networkDevice.Id == 0) DB.NetworkDevices.Set(networkDevice);
                    else DB.NetworkDevices.Update(networkDevice);
                    
                    break;
                }
                case "UchetVT.OS":
                {
                    OS os = (OS)this.DataContext;

                    if (os.Id == 0) DB.OSes.Set(os);
                    else DB.OSes.Update(os);
                    
                    break;
                }
                case "UchetVT.Printer":
                {
                    Printer printer = (Printer)this.DataContext;

                    if (printer.Id == 0) DB.Printers.Set(printer);
                    else DB.Printers.Update(printer);
                    
                    break;
                }
                case "UchetVT.Region":
                {
                    Region region = (Region)this.DataContext;

                    if (region.Id == 0) DB.Regions.Set(region);
                    else DB.Regions.Update(region);
                    
                    break;
                }
                case "UchetVT.UPS":
                {
                    UPS ups = (UPS)this.DataContext;

                    if (ups.Id == 0) DB.UPSes.Set(ups);
                    else DB.UPSes.Update(ups);

                    break;
                }
                case "UchetVT.User":
                {
                    User user = (User)this.DataContext;

                    if (user.Id == 0) DB.Users.Set(user);
                    else DB.Users.Update(user);
                    
                    break;
                }

                case "UchetVT.VTComputer":
                {
                    VTComputer vtComputer = (VTComputer) this.DataContext;

                        // TODO: нати решение получше!   
                            //Готовим подстановку Id по выбранным вариантам из комбо-боксов
                            vtComputer.BoardId = vtComputer.Board.Id = (DB.Boards.GetAll()
                                .FirstOrDefault(b => b.Motherboard == vtComputer.Board.Motherboard)).Id;

                            vtComputer.CpuId = vtComputer.Cpu.Id = (DB.CPUs.GetAll()
                                .FirstOrDefault(b => b.NameCPU == vtComputer.Cpu.NameCPU)).Id;

                            vtComputer.HddId = vtComputer.Hdd.Id = (DB.HDDs.GetAll()
                                .FirstOrDefault(b => b.NameHDD == vtComputer.Hdd.NameHDD)).Id;

                            vtComputer.LicenseId = vtComputer.License.Id = (DB.Licenses.GetAll()
                                .FirstOrDefault(b => b.LicenseState == vtComputer.License.LicenseState)).Id;

                            vtComputer.OsId = vtComputer.OS.Id = (DB.OSes.GetAll()
                                .FirstOrDefault(b => b.NameOS == vtComputer.OS.NameOS)).Id;

                            //Добавляем ид района
                            vtComputer.OwnerRegion = ((Region)((MainWindow)this.Owner).SideListView.SelectedItem).Id;
                            // MessageBox.Show(vtComputer.OwnerRegion.ToString());  //TODO Убрать!

                            if (vtComputer.Id == 0) DB.VTComputers.Set(vtComputer);
                            else DB.VTComputers.Update(vtComputer);



                        break;
                }

                case "UchetVT.VTPrinter":
                {
                    VTPrinter vtPrinter = (VTPrinter)this.DataContext;

                        // TODO: нати решение получше!   
                        //Готовим подстановку Id по выбранным вариантам из комбо-боксов
                        vtPrinter.PrinterId = vtPrinter.Printer.Id = (DB.Printers.GetAll()
                        .FirstOrDefault(b => b.NamePrinter == vtPrinter.Printer.NamePrinter)).Id;

                    //Добавляем ид района
                    vtPrinter.OwnerRegion = ((Region)((MainWindow)this.Owner).SideListView.SelectedItem).Id;
                    
                    if (vtPrinter.Id == 0) DB.VTPrinters.Set(vtPrinter);
                    else DB.VTPrinters.Update(vtPrinter);

                    break;
                }

                case "UchetVT.VTNetworkDevice":
                {
                    VTNetworkDevice vtNetworkDevice= (VTNetworkDevice)this.DataContext;

                        // TODO: нати решение получше!   
                        //Готовим подстановку Id по выбранным вариантам из комбо-боксов
                        vtNetworkDevice.DeviceId = vtNetworkDevice.NetworkDevice.Id = (DB.NetworkDevices.GetAll()
                        .FirstOrDefault(b => b.NameNetworkDevice == vtNetworkDevice.NetworkDevice.NameNetworkDevice)).Id;

                        //Добавляем ид района
                        vtNetworkDevice.OwnerRegion = ((Region)((MainWindow)this.Owner).SideListView.SelectedItem).Id;

                    if (vtNetworkDevice.Id == 0) DB.VTNetworkDevices.Set(vtNetworkDevice);
                    else DB.VTNetworkDevices.Update(vtNetworkDevice);

                    break;
                }

                case "UchetVT.VTUPS":
                {
                    VTUPS vtUPS = (VTUPS)this.DataContext;

                        // TODO: нати решение получше!   
                        //Готовим подстановку Id по выбранным вариантам из комбо-боксов
                        vtUPS.UPSId = vtUPS.UPS.Id = (DB.UPSes.GetAll()
                        .FirstOrDefault(b => b.NameUPS == vtUPS.UPS.NameUPS)).Id;

                        //Добавляем ид района
                        vtUPS.OwnerRegion = ((Region)((MainWindow)this.Owner).SideListView.SelectedItem).Id;

                    if (vtUPS.Id == 0) DB.VTUPSes.Set(vtUPS);
                    else DB.VTUPSes.Update(vtUPS);

                    break;
                }
            }

            this.DialogResult = true;
            Close();
        }

        private void RecordWindow_OnClosing(object sender, CancelEventArgs e)
        {
            GroupBox.Content = null;
        }

       
    }
}
