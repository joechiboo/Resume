﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Member
    {
        public int id { get; set; }
        public string Name { get; set; }
        public Guid Hash { get; set; }    // relation information

        public string SessionID { get; set; }   // template Device session
        public int? Valid { get; set; } // some people don't count
    }
}