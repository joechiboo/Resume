﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Resume.DAL;
using Resume.Models;
using Resume.Service;
using System.Web;

namespace Resume.Controllers.api
{
    public class MembersController : ApiController
    {
        private MarriedContext db = new MarriedContext();

        // GET: api/Members
        public IQueryable<String> GetMembers()
        {
            return db.Members.Select(p=>p.Name);
        }

        // GET: api/Members/5
        [ResponseType(typeof(Member))]
        public IHttpActionResult GetMember(int id)
        {
            Member member = _GetMember(id);
            if (member == null)
            {
                return NotFound();
            }

            member.Hash = Guid.Empty;   // hide

            return Ok(member);
        }

        private Member _GetMember(int id)
        {
            return db.Members.Find(id);
        }

        private Member _GetMember(string name)
        {
            return db.Members.Where(p => p.Name == name).FirstOrDefault();
        }

        // POST: api/Members
        [ResponseType(typeof(Member))]
        public IHttpActionResult PostMember(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Member _member = _GetMember(member.Name);

            if (_member == null)
            {
                member.Hash = Guid.NewGuid();

                db.Members.Add(member);
                db.SaveChanges();

                _member = _GetMember(member.Name);
            }

            db.SaveChanges();

            return Ok(_member);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberExists(int id)
        {
            return db.Members.Count(e => e.id == id) > 0;
        }
    }
}