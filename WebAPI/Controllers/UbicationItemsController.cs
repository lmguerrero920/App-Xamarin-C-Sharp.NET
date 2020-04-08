using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UbicationItemsController : ApiController
    {
        private Komodo1Entities db = new Komodo1Entities();

        // GET: api/UbicationItems
        public IQueryable<UbicationItems> GetUbicationItems()
        {
            return db.UbicationItems;
        }

        // GET: api/UbicationItems/5
        [ResponseType(typeof(UbicationItems))]
        public async Task<IHttpActionResult> GetUbicationItems(int id)
        {
            UbicationItems ubicationItems = await db.UbicationItems.FindAsync(id);
            if (ubicationItems == null)
            {
                return NotFound();
            }

            return Ok(ubicationItems);
        }

        // PUT: api/UbicationItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUbicationItems(int id, UbicationItems ubicationItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ubicationItems.Id)
            {
                return BadRequest();
            }

            db.Entry(ubicationItems).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbicationItemsExists(id))
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

        // POST: api/UbicationItems
        [ResponseType(typeof(UbicationItems))]
        public async Task<IHttpActionResult> PostUbicationItems(UbicationItems ubicationItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UbicationItems.Add(ubicationItems);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ubicationItems.Id }, ubicationItems);
        }

        // DELETE: api/UbicationItems/5
        [ResponseType(typeof(UbicationItems))]
        public async Task<IHttpActionResult> DeleteUbicationItems(int id)
        {
            UbicationItems ubicationItems = await db.UbicationItems.FindAsync(id);
            if (ubicationItems == null)
            {
                return NotFound();
            }

            db.UbicationItems.Remove(ubicationItems);
            await db.SaveChangesAsync();

            return Ok(ubicationItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UbicationItemsExists(int id)
        {
            return db.UbicationItems.Count(e => e.Id == id) > 0;
        }
    }
}