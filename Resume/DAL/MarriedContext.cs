using Resume.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resume.DAL
{
    public class MarriedContext : DbContext
    {
        public MarriedContext()
        {

        }
        public DbSet<ChatLog> ChatLogs { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Information> Infomations { get; set; }
        
        public DbSet<Table> Tables { get; set; }
    }
}