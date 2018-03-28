using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace birdCountServer.Domain
{
    public class Weather
    {
        public int Id { get; set;  } 
        public decimal LowTemp { get; set; }
        public decimal HighTemp { get; set; }
        public string WindDirection { get; set; }
        public decimal LowWind { get; set; }
        public decimal HighWind { get; set; }
        public decimal SnowMinDepth { get; set; }
        public decimal SnowMaxDepth { get; set; }
        public string StillWater { get; set; }
        public string MovingWater { get; set; }
        public string AMCloud { get; set; }
        public string PMCloud { get; set; }
        public string AMRain { get; set; }
        public string PMRain { get; set; }
        public string AMSnow { get; set; }
        public string PMSnow { get; set; }
    }
}