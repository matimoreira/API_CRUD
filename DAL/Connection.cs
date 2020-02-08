using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace API_CRUD
{
    public class Connection
    {
        private const string connString = "data source=104.217.253.86;initial catalog=Tracking;user id=alumno;password=12345678";

        private static SqlConnection conn = new SqlConnection(connString);

        public static SqlDataReader readInfo(string query)
        {
            conn.Open();
            var command = new SqlCommand(query, conn);
            return command.ExecuteReader();
        }
        public static int deleteInfo(string query)
        {
            conn.Open();
            var command = new SqlCommand(query, conn);
            return command.ExecuteNonQuery();
        }

        public static void disconnect()
        {
            conn.Close();
        }
    }
}