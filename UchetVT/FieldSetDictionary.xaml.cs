using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;

namespace UchetVT
{
    partial class FieldSetDictionary : ResourceDictionary
    {
        IUnitOfWork DB = new UnitOfWork();
        public FieldSetDictionary()
        {
            InitializeComponent();
        }

        private void boardbox_OnLoaded(object sender, RoutedEventArgs e)
        {
            //ComboBox boardCombo = (ComboBox)sender;
           // var boards = DB.Boards.GetAll();
           // var boardsNames = boards.Select(b => b.Motherboard);

            // boardCombo.ItemsSource = boardsNames;
            //boardCombo.ItemsSource = boards;

            ((ComboBox) sender).ItemsSource = DB.Boards.GetAll().Select(b => b.Motherboard);
        }

        private void cpubox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox cpuCombo = (ComboBox)sender;
            var cpus = DB.CPUs.GetAll();
            var cpuNames = cpus.Select(c => c.NameCPU);

            cpuCombo.ItemsSource = cpuNames;
        }

        private void hddbox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox hddCombo = (ComboBox)sender;
            var hdds = DB.HDDs.GetAll();
            var hddNames = hdds.Select(h => h.NameHDD);

            hddCombo.ItemsSource = hddNames;

        }

        private void osbox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox osCombo = (ComboBox)sender;

            var oses = DB.OSes.GetAll();
            var osNames = oses.Select(o => o.NameOS);

            osCombo.ItemsSource = osNames;
        }

        private void licensebox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox licenseCombo = (ComboBox)sender;

            var licenses = DB.Licenses.GetAll();
            var licenseStates = licenses.Select(l => l.LicenseState);

            licenseCombo.ItemsSource = licenseStates;
        }

        private void networkdevicebox_OnLoad(object sender, RoutedEventArgs e)
        {
            ComboBox netDevCombo = (ComboBox) sender;

            var netdevs = DB.NetworkDevices.GetAll();
            var netdevNames = netdevs.Select(n => n.NameNetworkDevice);

            netDevCombo.ItemsSource = netdevNames;
        }

        private void printerbox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox printerCombo = (ComboBox)sender;

            var printers = DB.Printers.GetAll();
            var printerNames = printers.Select(p => p.NamePrinter);

            printerCombo.ItemsSource = printerNames;
        }

        private void upsbox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox upsCombo = (ComboBox)sender;

            var upses = DB.UPSes.GetAll();
            var upsNames = upses.Select(p => p.NameUPS);

            upsCombo.ItemsSource = upsNames;
        }
    }
}
