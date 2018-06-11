using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace cinema_i_s.Classes
{
    public class Query4Data
    {
        public DateTimeOffset data { get; set; }

        public TimeSpan time { get; set; }
        public string name_hall { get; set; }
        public string capacity { get; set; }
        
    }
}