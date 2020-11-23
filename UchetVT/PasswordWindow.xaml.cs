using System;
using System.Collections.Generic;
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
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public string Password { get; set; }
        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {

            if (Pass.Text == PassConfirm.Text && string.IsNullOrEmpty(Pass.Text))
            {
                Password = Pass.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Неверно введен пароль\nПопробуйте ещё раз", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Pass.Text = PassConfirm.Text = "";
            }
        }
    }
}
