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
using EventTicketingApp;

namespace EventTicketingApp.Controllers
{
    public class EventTicketsController : ApiController
    {
        private EventTicketingAppEntities db = new EventTicketingAppEntities();

        // GET: api/EventTickets
        public IQueryable<EventTicket> GetEventTickets()
        {
            return db.EventTickets;
        }

        // GET: api/EventTickets/5
        [ResponseType(typeof(EventTicket))]
        public IHttpActionResult GetEventTicket(int id)
        {
            EventTicket eventTicket = db.EventTickets.Find(id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            return Ok(eventTicket);
        }

        // PUT: api/EventTickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventTicket(int id, EventTicket eventTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventTicket.EventId)
            {
                return BadRequest();
            }

            db.Entry(eventTicket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EventTickets
        [ResponseType(typeof(EventTicket))]
        public IHttpActionResult PostEventTicket(EventTicket eventTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventTickets.Add(eventTicket);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventTicket.EventId }, eventTicket);
        }

        // DELETE: api/EventTickets/5
        [ResponseType(typeof(EventTicket))]
        public IHttpActionResult DeleteEventTicket(int id)
        {
            EventTicket eventTicket = db.EventTickets.Find(id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            db.EventTickets.Remove(eventTicket);
            db.SaveChanges();

            return Ok(eventTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventTicketExists(int id)
        {
            return db.EventTickets.Count(e => e.EventId == id) > 0;
        }
    }
}