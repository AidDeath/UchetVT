using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace UchetVT
{
   public class ServerConnection
   {
       public string ConnectionString;
       public  void ConnectToServer(User user)
        {
            ConnectionString = 
                $"Data Source = {Properties.Settings.Default.ServerHostname}\\{Properties.Settings.Default.SQLInstance};" +
                $" Persist Security Info = True; " +
                $"Password = {user.UserPassword};" +
                $" User ID = {user.UserName};" +
                $" Initial Catalog = {Properties.Settings.Default.Database}";


        }

 


    }
}
