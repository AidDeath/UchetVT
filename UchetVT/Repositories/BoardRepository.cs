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
    public class BoardRepository : IRepository<Board>
    {
        public ObservableCollection<Board> GetAll()
        {
            ObservableCollection<Board> boards = new ObservableCollection<Board>();
            DataTable boardsTable = DatabaseUtility.GetTable("SELECT * FROM BookBoard");
            foreach (DataRow row in boardsTable.Rows)
            {
                boards.Add(new Board()
                {
                    Id = row.Field<int>("Id"),
                    Motherboard = row.Field<string>("Motherboard"),
                    YearOut = row.Field<int?>("YearOut")
                });
            }

            return boards;
        }


        public Board Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(Board model)
        {
            DatabaseUtility.Exec("INSERT INTO BookBoard ( Motherboard, YearOut) VALUES (@Motherboard, @YearOut)", new List<SqlParameter>()
            {
                new SqlParameter("@Motherboard",(object)model.Motherboard ?? DBNull.Value),
                new SqlParameter("@YearOut", (object)model.YearOut ?? DBNull.Value)
            });
        }

        public void Update(Board model)
        {
            DatabaseUtility.Exec("UPDATE BookBoard SET  Motherboard = @Motherboard , YearOut = @YearOut  WHERE Id = @Id", new List<SqlParameter>()
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@Motherboard", (object)model.Motherboard ?? DBNull.Value),
                new SqlParameter("@YearOut", (object)model.YearOut ?? DBNull.Value)
            });
        }

        public void Delete(Board model)
        {
            DatabaseUtility.Exec("DELETE FROM BookBoard WHERE Id = @Id",
                new List<SqlParameter>()
                {
                    new SqlParameter("@Id", model.Id)
                });
        }

        public GridView MakeView()
        {
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Ид", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Id") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Модель платы", DisplayMemberBinding = new Binding() { Path = new PropertyPath("Motherboard") } });
            gridView.Columns.Add(new GridViewColumn() { Header = "Год выхода", DisplayMemberBinding = new Binding() { Path = new PropertyPath("YearOut") } });
            return gridView;
        }

    }
}
