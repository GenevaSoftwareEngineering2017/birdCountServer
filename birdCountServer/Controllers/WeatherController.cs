using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Text;
using birdCountServer.Domain;


namespace WeatherServer.Controllers
{
    [RoutePrefix("api/Weather")]
    public class WeatherController : ApiController
    {
        [Route("getall")]
        public List<Weather> GetAll()
        {
            return RetrieveWeatherFromDB(string.Empty);
        }

        [Route("getWeatherbyid/{id}")]
        public Weather GetWeather(int id)
        {
            return RetrieveWeatherFromDB(string.Format("WeatherID = {0}", id)).FirstOrDefault();
        }

        //[Route("getbirdbyname/{name}")]
        //public Weather GetWeather(string name)
        //{
        //    return RetrieveWeatherFromDB(string.Format("BirdName = '{0}'", name)).FirstOrDefault();
        //}

        public List<Weather> RetrieveWeatherFromDB(string whereClause)
        {
            var retWeather = new List<Weather>();
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

                    sb.Append(string.Format("select * from Weather "));

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
                                var add = new Weather();
                                add.Id = reader.GetInt32(0);
                                add.LowTemp = reader.GetDecimal(1);
                                add.HighTemp = reader.GetDecimal(2);
                                add.WindDirection = reader.GetString(3);
                                add.LowWind = reader.GetDecimal(4);
                                add.HighWind = reader.GetDecimal(5); 
                                add.SnowMinDepth = reader.GetDecimal(6);
                                add.SnowMaxDepth = reader.GetDecimal(7);
                                add.StillWater = reader.GetString(8);
                                add.MovingWater = reader.GetString(9);
                                add.AMCloud = reader.GetString(10);
                                add.PMCloud = reader.GetString(11);
                                add.AMRain = reader.GetString(12);
                                add.PMRain = reader.GetString(13);
                                add.AMSnow = reader.GetString(14);
                                add.PMSnow = reader.GetString(15);
                                retWeather.Add(add);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return retWeather;

        }
    }
}
