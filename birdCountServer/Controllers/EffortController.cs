using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Text;
using birdCountServer.Domain;


namespace EffortCountServer.Controllers
{
    [RoutePrefix("api/effort")]
    public class EffortController : ApiController
    {

        [Route("getall")]
        public List<Effort> GetAll()
        {
            return RetrieveEffortFromDB(string.Empty);
        }

        [Route("geteffortbyid/{id}")]
        public Effort GetEffort(int id)
        {
            return RetrieveEffortFromDB(string.Format("TEAMID = {0}", id)).FirstOrDefault();
        }

        public List<Effort> RetrieveEffortFromDB(string whereClause)
        {
            var retEffort = new List<Effort>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "birdcount.database.windows.net";
                builder.UserID = "jmgirvin";
                builder.Password = "Devan2/12/16";
                builder.InitialCatalog = "BirdCountDatabase";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(string.Format("select * from Effort "));

                    if (!string.IsNullOrEmpty(whereClause))
                    {
                        sb.Append("WHERE " + whereClause);
                    }

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var add = new Effort();
                                add.Id = reader.GetInt32(0);
                                add.CountStart = reader.GetTimeSpan(1).ToString();
                                add.CountEnd = reader.GetTimeSpan(2).ToString();
                                add.DayMilesDriven = reader.GetInt32(3);
                                add.DayHoursDriven = reader.GetInt32(4);
                                add.DayMilesWalked = reader.GetInt32(5);
                                add.DayHoursWalked = reader.GetInt32(6);
                                add.MilesOwling = reader.GetInt32(7);
                                add.HoursOwling = reader.GetInt32(8);
                                add.TeamId= reader.GetInt32(9);
                                retEffort.Add(add);
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
