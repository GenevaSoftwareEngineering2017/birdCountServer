using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Text;


namespace birdCountServer.Controllers
{
    public class EffortController : ApiController
    {
        public string Get()
        {
            var retEffort = string.Empty;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "birdcountapp.database.windows.net";
                builder.UserID = "User";
                builder.Password = "Password1";
                builder.InitialCatalog = "birdCountApp";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select * from Effort");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                retEffort = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return retEffort;

        }
    }
}
