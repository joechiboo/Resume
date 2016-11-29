using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class ChatLog
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public String Message { get; set; }

        public DateTime LogTime { get; set; }
    }
}