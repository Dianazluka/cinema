﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema_i_s.Classes
{
    public class Query3Data
    {
        public string full_name { get; set; }

        public string position { get; set; }
        public string id_film { get; set; }
        public string seance_id { get; set; }

        public string id_hall { get; set; }
        public int price { get; set; }
        public string movie_format { get; set; }

        public DateTimeOffset data { get; set; }
        public DateTimeOffset time { get; set; }
    }
}