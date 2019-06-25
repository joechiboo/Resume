using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Resume.Service
{
    public class MessageService
    {
        private string _connection = ConfigurationManager.ConnectionStrings["ToeicConnect"].ConnectionString;
        public void MessageLog(string name, string message)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT [Messages] (name, [text], CreateTime)VALUES(
	                    @name, @msg, DATEADD(HOUR, 15, GETDATE())
                    )
                ", conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@msg", System.Data.SqlDbType.VarChar).Value = message;
                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
    }
}