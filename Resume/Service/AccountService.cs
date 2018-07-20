using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Resume.Service
{
    public class AccountService
    {
        private string _connection = ConfigurationManager.ConnectionStrings["ToeicConnect"].ConnectionString;
        public int Login(string name)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    DECLARE @Id INT                    

                    SELECT TOP 1 @Id = Id FROM Account AS a WITH(NOLOCK)
                    WHERE name = @name

                    IF @Id IS NULL
                    BEGIN
                        INSERT Account(name, CreateTime)VALUES(@name, DATEADD(HOUR, 15, GETDATE()))	-- 15: timezone diff
                        SELECT @Id = SCOPE_IDENTITY()
                    END

                    SELECT @Id AS id
                ", conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null ? (int)result : 0;
                }
            }
        }
    }
}
