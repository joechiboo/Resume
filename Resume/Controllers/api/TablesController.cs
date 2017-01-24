using Resume.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Resume.Controllers.api
{
    public class TablesController : ApiController
    {
        private MarriedContext db = new MarriedContext();

        public IQueryable<string> GetTalkTables() {
            var result = from c in db.ChatLogs
                         join m in db.Members on c.MemberId equals m.id
                         join i in db.Infomations on c.MemberId equals i.memberid
                         where m.Valid == 1 
                            && i.tableid != null
                         select i.tableid;

            return result.Distinct();
        }
    }
}
