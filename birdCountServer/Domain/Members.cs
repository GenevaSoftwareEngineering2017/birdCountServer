using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace birdCountServer.Domain
{
    public class Members
    {
        public int Id { get; set; }
        public string FullName { get; set; }
       // public string Method { get; set; }
        public string HomeAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TeamID { get; set; }
    }
}