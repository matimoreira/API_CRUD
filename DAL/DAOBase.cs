using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API_CRUD
{
    public class DAOBase
    {
        private const string connString = "data source=104.217.253.86;initial catalog=Tracking;user id=alumno;password=12345678";
        private readonly SqlConnection conn;

        public DAOBase()
        {
            conn = new SqlConnection(connString);
            conn.Open();
        }
        ~DAOBase()
        {
            conn.Close();
        }

        protected SqlDataReader GetReader(string query)
        {
            var command = new SqlCommand(query, conn);
            return command.ExecuteReader();
        }
        protected int GetNonQuery(string query)
        {
            var command = new SqlCommand(query, conn);
            return command.ExecuteNonQuery();
        }

    }
}
