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
    [RoutePrefix("api/member")]
    public class Team_MembersController : ApiController
    {


        [Route("getall")]
        public List<Members> GetAll()
        {
            return RetrieveMembersFromDB(string.Empty);
        }

        [Route("getbirdbyid/{id}")]
        public Members GetMembers(int id)
        {
            return RetrieveMembersFromDB(string.Format("MemberId = {0}", id)).FirstOrDefault();
        }

        [Route("getbirdbyname/{name}")]
        public Members Get(string name)
        {
            return RetrieveMembersFromDB(string.Format("FullName = '{0}'", name)).FirstOrDefault();
        }

        public List<Members> RetrieveMembersFromDB(string whereClause)
        {
            var retMembers = new List<Members>();
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

                    sb.Append(string.Format("select * from Team_Members "));

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
                                var add = new Members();
                                add.Id = reader.GetInt32(0);
                                add.FullName = reader.GetString(1);
                                add.HomeAddress = reader.GetString(2);
                                add.Email = reader.GetString(3);
                                add.PhoneNumber = reader.GetString(4);
                                add.TeamID = reader.GetInt32(5);                               
                                retMembers.Add(add);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return retMembers;
        }
    }
}
   