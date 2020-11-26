using System.Collections.Generic;
using System.Data.SqlClient;

namespace UchetVT
{
    class ConsoleHelper
    {
        static void Main(string[] args)
        {
            int a = DatabaseUtility.ExecStorageProcWithReturnValue("Test", new List<SqlParameter>());
        }
    }
}
