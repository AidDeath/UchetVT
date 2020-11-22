using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UchetVT
{
    /// <summary>
    /// Класс помощник для доступа к базе данных
    /// </summary>
    public class DatabaseUtility
    {
        private static string connectionstring = (string)System.Windows.Application.Current.Properties["ConnectionString"];

        /// <summary>
        /// Установка строки подключения
        /// </summary>
        /// <param name="connectionString">строка соединения</param>
        public static void SetConnectionString(string connectionString)
        {
            connectionstring = connectionString;
        }

        /// <summary>
        /// Открытие соединия с базой данных
        /// </summary>
        /// <param name="connectionString">строка подключения к бд</param>
        /// <returns></returns>
        public static void OpenConnection(string connectionString)
        {
            connectionstring = connectionString;

            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получение записей из бд используя параметры 
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns>Объект с данными</returns>
        public static SqlDataReader Get(string query, List<SqlParameter> parameters, CommandBehavior behavior = CommandBehavior.Default)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(query, Connection);

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    return Command.ExecuteReader(behavior);
                }
                catch (SqlException ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// Получение записей из бд используя параметры 
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns>Объект с данными</returns>
        public static SqlDataReader Get(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(query, Connection);

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    Command.CommandType = CommandType.Text;
                    return Command.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получение записей из бд используя параметры 
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns>Объект с данными</returns>
        public static DataTable GetTable(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(query, Connection);

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    Command.CommandType = CommandType.Text;

                    DataTable table = new DataTable();
                    SqlDataReader reader = Command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();

                    return table;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получение записей из бд используя параметры 
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <returns>Объект с данными</returns>
        public static DataTable GetTable(string query)
        {
            using  (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(query, Connection);

                    Command.CommandType = CommandType.Text;

                    DataTable table = new DataTable();
                    SqlDataReader reader = Command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();

                    return table;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получение записей из бд используя параметры 
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <returns>Объект с данными</returns>
        public static SqlDataReader Get(string query)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(query,  Connection);

                    Command.CommandType = CommandType.Text;
                    return Command.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// Получение записей из бд через вызов хранимой процедуры
        /// </summary>
        /// <param name="query">хранимая процедура</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns>Объект с данными</returns>
        public static SqlDataReader GetStorageProc(string storagerpoc, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(storagerpoc, Connection);
                    Command.CommandType = System.Data.CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    
                    return Command.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получение записей из бд через вызов хранимой процедуры
        /// </summary>
        /// <param name="query">хранимая процедура</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns>Объект с данными</returns>
        public static DataTable GetStorageProcTable(string storagerpoc, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(storagerpoc, Connection);
                    Command.CommandType = System.Data.CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }


                    SqlDataReader reader = Command.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(reader);

                    return table;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получение записей из бд через вызов хранимой процедуры
        /// </summary>
        /// <param name="query">хранимая процедура</param>
        /// <returns>Объект с данными</returns>
        public static DataTable GetStorageProcTable(string storagerpoc)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(storagerpoc, Connection);
                    Command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = Command.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(reader);

                    return table;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Запись в базу используя параметры
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns></returns>
        public static int Exec(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(query, Connection);
                    
                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    return Command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Запись в базу через вызов хранимой процедуры
        /// </summary>
        /// <param name="query">хранимая процедура</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns></returns>
        public static void ExecStorageProc(string storageproc, List<SqlParameter> parameters)
        {
            using (var Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    var Command = new SqlCommand(storageproc, Connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    Command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        ///   в базу через вызов хранимой процедуры
        /// </summary>
        /// <param name="query">хранимая процедура</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns></returns>
        public static int ExecStorageProcScalar(string storageproc, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(storageproc, Connection);
                    Command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    return Convert.ToInt32(Command.ExecuteScalar());
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        ///   в базу через вызов хранимой процедуры
        /// </summary>
        /// <param name="query">хранимая процедура</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns></returns>
        public static int ExecStorageProcWithReturnValue(string storageproc, List<SqlParameter> parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    SqlCommand Command = new SqlCommand(storageproc, Connection);
                    Command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    SqlParameter retval = Command.Parameters.Add("@return_value", SqlDbType.Int);
                    retval.Direction = ParameterDirection.ReturnValue;

                    parameters.Add(retval);

                    Command.ExecuteNonQuery();
                    return (int)Command.Parameters["@return_value"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Выполение специальных встроенных функций SQL (min, max, count)
        /// </summary>
        /// <param name="query">строка запроса</param>
        /// <param name="parameters">коллекция параметров</param>
        /// <returns>результат работы</returns>
        public static object Scalar(string query, List<SqlParameter> parameters = null)
        {
            using (var Connection = new SqlConnection(connectionstring))
            {
                try
                {
                    Connection.Open();

                    var Command = new SqlCommand(query, Connection)
                    {
                        CommandType = CommandType.Text
                    };

                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters.ToArray());
                    }

                    return Command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
