using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace piris.DomainService
{
    class DB
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=RUSLANALAPTOP;Initial Catalog=TPCR_DB;Integrated Security=True");
        public void openConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
        public void ExecuteNonQuery(string query)
        {
            openConnection(); // Открываем соединение с базой данных
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.ExecuteNonQuery(); // Выполняем SQL-запрос
            closeConnection(); // Закрываем соединение с базой данных
        }
    }
}
