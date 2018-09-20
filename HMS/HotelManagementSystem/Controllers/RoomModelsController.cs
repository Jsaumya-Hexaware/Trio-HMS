using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class RoomModelsController : Controller
    {
        private RoomBD db = new RoomBD();

        // GET: RoomModels
        public async Task<ActionResult> Index()
        {
            return View(await db.Rooms.ToListAsync());
        }

        // GET: RoomModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModel roomModel = await db.Rooms.FindAsync(id);
            if (roomModel == null)
            {
                return HttpNotFound();
            }
            return View(roomModel);
        }

        // GET: RoomModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Room_ID,Room_Type,Room_Available")] RoomModel roomModel)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(roomModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(roomModel);
        }

        // GET: RoomModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModel roomModel = await db.Rooms.FindAsync(id);
            if (roomModel == null)
            {
                return HttpNotFound();
            }
            return View(roomModel);
        }

        // POST: RoomModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Room_ID,Room_Type,Room_Available")] RoomModel roomModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(roomModel);
        }

        // GET: RoomModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModel roomModel = await db.Rooms.FindAsync(id);
            if (roomModel == null)
            {
                return HttpNotFound();
            }
            return View(roomModel);
        }

        // POST: RoomModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomModel roomModel = await db.Rooms.FindAsync(id);
            db.Rooms.Remove(roomModel);
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
