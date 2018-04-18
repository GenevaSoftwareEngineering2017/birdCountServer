using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Text;
using birdCountServer.Domain;

namespace birdCountServer.Controllers
{
    [RoutePrefix("api/count")]
    public class CountController : ApiController
    {


        [Route("getall")]
        public List<Count> GetAll()
        {
            return RetrieveCountFromDB(string.Empty);
        }

        [Route("getbirdbyid/{id}")]
        public Count GetCount(int id)
        {
            return RetrieveCountFromDB(string.Format("CountId = {0}", id)).FirstOrDefault();
        }

        [Route("getbirdbyname/{name}")]
        public Count Get(string name)
        {
            return RetrieveCountFromDB(string.Format("FullName = '{0}'", name)).FirstOrDefault();
        }

        public List<Count> RetrieveCountFromDB(string whereClause)
        {
            var retCount = new List<Count>();
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

                    sb.Append(string.Format("select * from Count "));

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
                                var add = new Count();
                                add.Id = reader.GetInt32(0);
                                add.TeamId = reader.GetInt32(1);
                                add.BirdId = reader.GetInt32(2);
                                add.TeamName = reader.GetString(3);
                                add.BirdName = reader.GetString(4);
                                add.BirdCount = reader.GetInt32(5);
                                retCount.Add(add);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return retCount;
        }
    }
}
