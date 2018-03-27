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
    [RoutePrefix("api/bird")]
    public class BirdController : ApiController
    {
        [Route("getall")]
        public List<Bird> GetAll()
        {
            return RetrieveBirdsFromDB(string.Empty);
        }

        [Route("getbirdbyid/{id}")]
        public Bird GetBird(int id)
        {
            return RetrieveBirdsFromDB(string.Format("BirdId = {0}", id)).FirstOrDefault();
        }

        [Route("getbirdbyname/{name}")]
        public Bird GetBird(string name)
        {
            return RetrieveBirdsFromDB(string.Format("BirdName = '{0}'", name)).FirstOrDefault();
        }

        public List<Bird> RetrieveBirdsFromDB(string whereClause)
        {
            var retBird = new List<Bird>();
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
                    
                    sb.Append(string.Format("select * from Birds "));

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
                                var add = new Bird();
                                add.Id = reader.GetInt32(0);
                                add.Name = reader.GetString(1);
                                retBird.Add(add);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return retBird;
        }
    }
}