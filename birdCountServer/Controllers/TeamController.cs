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
    [RoutePrefix("api/team")]
    public class TeamController : ApiController
    {
        [Route("getall")]
        public List<Team> GetAll()
        {
            return RetrieveTeamsFromDB(string.Empty);
        }

        [Route("getteambyid/{id}")]
        public Team GetTeam(int id)
        {
            return RetrieveTeamsFromDB(string.Format("TeamId = {0}", id)).FirstOrDefault();
        }

        [Route("getteambyname/{name}")]
        public Team GetTeam(string name)
        {
            return RetrieveTeamsFromDB(string.Format("TeamName = '{0}'", name)).FirstOrDefault();
        }

        [Route("getteambymethod/{method}")]
        public Team GetMethod(string method)
        {
            return RetrieveTeamsFromDB(string.Format("MethodType = '{0}'", method)).FirstOrDefault();
        }

        public List<Team> RetrieveTeamsFromDB(string whereClause)
        {
            var retTeam = new List<Team>();
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

                    sb.Append(string.Format("select * from Teams "));

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
                                var add = new Team();
                                add.Id = reader.GetInt32(0);
                                add.Name = reader.GetString(1);
                                add.Method = reader.GetString(2);
                                retTeam.Add(add);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return retTeam;
        }
    }
}