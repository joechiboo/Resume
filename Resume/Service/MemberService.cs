using Resume.DAL;
using Resume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Service
{
    public class MemberService
    {
        private MarriedContext db = new MarriedContext();

        public Member GetMember(string name)
        {
            return db.Members.Where(p => p.Name == name).FirstOrDefault();
        }
    }
}