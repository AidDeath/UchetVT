﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UchetVT
{
    /// <summary>
    /// Interaction logic for AccessEditWindow.xaml
    /// </summary>
    public partial class AccessEditWindow : Window
    {
        public enum WorkWithVariants { Region, Book }
        public WorkWithVariants WorkWith;

        public string AccessString;

        IUnitOfWork DB = new UnitOfWork();

        ObservableCollection<AccessData> AccessCollection = new ObservableCollection<AccessData>();


        public AccessEditWindow()
        {
            InitializeComponent();
            // IUnitOfWork DB = new UnitOfWork();
        }

        private void AccessEditWindow_OnLoaded(object sender, RoutedEventArgs e)
        {


            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn
            { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });

            switch (WorkWith)
            {
                case WorkWithVariants.Region:   // Выбор данных для региона
                    {
                        gridView.Columns.Add(new GridViewColumn
                        { Header = "Город", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameCity") } });

                        string[] regionsAccessArray = AccessString.Split(new char[] { ',' });

                        foreach (var region in DB.Regions.GetAll())
                        {
                            AccessCollection.Add(new AccessData()
                            {
                                Id = region.Id,
                                NameCity = region.NameCity,
                                Access = regionsAccessArray.Any(r => r == region.Id.ToString())
                            });
                        }
                        AccessEditView.ItemsSource = AccessCollection;
                        break;
                    }

                case WorkWithVariants.Book: // Выбор данных для справочника
                    {
                        gridView.Columns.Add(new GridViewColumn
                        { Header = "Справочник", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameBook") } });

                        string[] bookAccessArray = AccessString.Split(',');
                        
                        ObservableCollection<Book> books = new ObservableCollection<Book>();
                        DataTable bookTable = DatabaseUtility.GetTable("SELECT * FROM BookBooks");
                        foreach (DataRow row in bookTable.Rows)
                        {
                            books.Add(new Book()
                            {
                                Id = row.Field<int>("Id"),
                                NameBook = row.Field<string>("NameBook")
                            });
                        }
                        
                        foreach (var book in books)
                        {
                            AccessCollection.Add(new AccessData()
                            {
                                Id = book.Id,
                                NameBook = book.NameBook,
                                Access = bookAccessArray.Any(r => r == book.Id.ToString())
                            });
                        }
                        AccessEditView.ItemsSource = AccessCollection;
                        break;
                    }
            }

            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory elementFactory = new FrameworkElementFactory(typeof(CheckBox));
            elementFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding() { Path = new PropertyPath("Access"), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            dataTemplate.VisualTree = elementFactory;
            gridView.Columns.Add(new GridViewColumn { Header = "Доступ", CellTemplate = dataTemplate });

            AccessEditView.View = gridView;
        }

        private void AccessEditWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var resultArray = AccessCollection.Where(c => c.Access == true).Select(c => c.Id.ToString()).ToArray();

            AccessString = string.Join(",", resultArray);
        }

        private void ConfirmButton_Pressed(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class AccessData
    {
        public int Id { get; set; }
        public string NameCity { get; set; }
        public string NameBook { get; set; }
        public bool Access { get; set; }
    }

}
