using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace birdCountServer.Domain
{
    public class Count
    {
       
            public int Id { get; set; }
            public int BirdId { get; set; }
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string BirdName { get; set; }
            public int BirdCount { get; set; }

    }
}