using System;
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

namespace Resume.Controllers.api
{
    public class ChatController : ApiController
    {
        private MarriedContext db = new MarriedContext();

        // GET: api/Chat
        public IQueryable<ChatLog> GetChatLogs()
        {
            return db.ChatLogs;
        }

        // GET: api/Chat/5
        [ResponseType(typeof(ChatLog))]
        public IHttpActionResult GetChatLog(int id)
        {
            ChatLog chatLog = db.ChatLogs.Find(id);
            if (chatLog == null)
            {
                return NotFound();
            }

            return Ok(chatLog);
        }

        // POST: api/Chat
        [ResponseType(typeof(ChatLog))]
        public IHttpActionResult PostChatLog(ChatLog chatLog)
        {
            chatLog.LogTime = DateTime.Now;

            db.ChatLogs.Add(chatLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chatLog.Id }, chatLog);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChatLogExists(int id)
        {
            return db.ChatLogs.Count(e => e.Id == id) > 0;
        }
    }
}