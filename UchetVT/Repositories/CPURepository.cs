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
   public  class CPURepository : IRepository<CPU>
    {
        public ObservableCollection<CPU> GetAll()
        {
            ObservableCollection<CPU> cpus = new ObservableCollection<CPU>();
            DataTable cpuTable = DatabaseUtility.GetTable("SELECT * FROM BookCPU");
            foreach (DataRow row in cpuTable.Rows)
            {
                cpus.Add(new CPU()
                {
                    Id = row.Field<int>("Id"),
                    NameCPU = row.Field<string>("NameCPU")
                });
            }

            return cpus;
        }


        public CPU Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(CPU model)
        {
            DatabaseUtility.Exec("INSERT INTO BookCPU ( NameCPU) VALUES (@Motherboard)", new List<SqlParameter>()
            {
                new SqlParameter("@Motherboard", model.NameCPU)
            });
        }

        public void Update(CPU model)
        {
            DatabaseUtility.Exec("UPDATE BookCPU SET NameCPU=@NameCPU WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("Id", model.Id),
                new SqlParameter("@NameCPU", model.NameCPU)
            });
        }

        public void Delete(CPU model)
        {
            DatabaseUtility.Exec("DELETE FROM BookCPU WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Модель процессора", DisplayMemberBinding = new Binding() { Path = new PropertyPath("NameCPU") } });
            return gridView;
        }
    }
}
