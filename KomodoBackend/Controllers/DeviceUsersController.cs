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
    public class DeviceUsersController : Controller
    {
        private KomodoDeviceUsers1 db = new KomodoDeviceUsers1();

        // GET: DeviceUsers
        public async Task<ActionResult> Index()
        {
            return View(await db.DeviceUsers.ToListAsync());
        }

        // GET: DeviceUsers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceUsers deviceUsers = await db.DeviceUsers.FindAsync(id);
            if (deviceUsers == null)
            {
                return HttpNotFound();
            }
            return View(deviceUsers);
        }

        // GET: DeviceUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceUsers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdDeviceUsers,Nombre,Apellido,Usuario,Clave")] DeviceUsers deviceUsers)
        {
            if (ModelState.IsValid)
            {
                db.DeviceUsers.Add(deviceUsers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(deviceUsers);
        }

        // GET: DeviceUsers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceUsers deviceUsers = await db.DeviceUsers.FindAsync(id);
            if (deviceUsers == null)
            {
                return HttpNotFound();
            }
            return View(deviceUsers);
        }

        // POST: DeviceUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdDeviceUsers,Nombre,Apellido,Usuario,Clave")] DeviceUsers deviceUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceUsers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(deviceUsers);
        }

        // GET: DeviceUsers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceUsers deviceUsers = await db.DeviceUsers.FindAsync(id);
            if (deviceUsers == null)
            {
                return HttpNotFound();
            }
            return View(deviceUsers);
        }

        // POST: DeviceUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DeviceUsers deviceUsers = await db.DeviceUsers.FindAsync(id);
            db.DeviceUsers.Remove(deviceUsers);
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
