using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KomodoBackend.Models;

namespace KomodoBackend.Controllers
{
    public class UbicationItemsController : Controller
    {
        private Komodo1Entities1 db = new Komodo1Entities1();

        // GET: UbicationItems
        public async Task<ActionResult> Index()
        {
            return View(await db.UbicationItems.ToListAsync());
        }

        // GET: UbicationItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UbicationItems ubicationItems = await db.UbicationItems.FindAsync(id);
            if (ubicationItems == null)
            {
                return HttpNotFound();
            }
            return View(ubicationItems);
        }

        // GET: UbicationItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UbicationItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Phone,Address,Latitude,Longitude")] UbicationItems ubicationItems)
        {
            if (ModelState.IsValid)
            {
                db.UbicationItems.Add(ubicationItems);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ubicationItems);
        }

        // GET: UbicationItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UbicationItems ubicationItems = await db.UbicationItems.FindAsync(id);
            if (ubicationItems == null)
            {
                return HttpNotFound();
            }
            return View(ubicationItems);
        }

        // POST: UbicationItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Phone,Address,Latitude,Longitude")] UbicationItems ubicationItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicationItems).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ubicationItems);
        }

        // GET: UbicationItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UbicationItems ubicationItems = await db.UbicationItems.FindAsync(id);
            if (ubicationItems == null)
            {
                return HttpNotFound();
            }
            return View(ubicationItems);
        }

        // POST: UbicationItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UbicationItems ubicationItems = await db.UbicationItems.FindAsync(id);
            db.UbicationItems.Remove(ubicationItems);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
