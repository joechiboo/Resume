using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Information
    {
        [Key]
        public Guid hash { get; set; }  // member hash key
        public int memberid { get; set; }

        public bool accept { get; set; }

        public bool side { get; set; }

        public int number { get; set; } // 人數

        public int children { get; set; }

        public int vegetable { get; set; }

        public string address { get; set; }

        public bool bus { get; set; }

        public bool HSR { get; set; }
    }
}