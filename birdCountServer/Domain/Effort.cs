using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace birdCountServer.Domain
{
    public class Effort
    {
        public int Id { get; set; }
        public string CountStart { get; set; }
        public string CountEnd { get; set; }
        public double DayMilesDriven { get; set; }
        public double DayHoursDriven { get; set; }
        public double DayMilesWalked { get; set; }
        public double DayHoursWalked { get; set; }
        public double MilesOwling { get; set; }
        public double HoursOwling { get; set; }
        public int TeamId { get; set; }
    }
}