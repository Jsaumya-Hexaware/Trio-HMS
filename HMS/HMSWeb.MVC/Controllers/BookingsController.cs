using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectClassLibrary;
using HMS.Services;

namespace HMSWeb.MVC.Controllers
{
    public class BookingsController : Controller
    {
        private hmsprojectEntities db = new hmsprojectEntities();
        private readonly ICustomer cust;

        public BookingsController(ICustomer cust)
        {
            this.cust = cust;
        }

        // GET: Bookings
        public ActionResult Index()
        {
            int id = (int)Session["id"];
            var bookings = db.Bookings.Where(b => b.CustomerId == id).ToList();

            return View(bookings);
        }

        // GET: Bookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Firstname");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomType");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookingId,RoomId,CustomerId,checkIn,checkOut,BookedOn,payment")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Firstname", booking.CustomerId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomType", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Firstname", booking.CustomerId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomType", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookingId,RoomId,CustomerId,checkIn,checkOut,BookedOn,payment")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Firstname", booking.CustomerId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomType", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Booking booking = await db.Bookings.FindAsync(id);
            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Bookings/Create
        public ActionResult RoomBook(string type, string Description, int? cost)
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Firstname", Session["id"]);
            Session["RoomType"] = type;
            ViewBag.Description = Description;
            ViewBag.Cost = cost;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoomBook(Booking booking,string payment,string fromDate,string toDate)
        {
            booking.RoomType = (string)Session["RoomType"];
            booking.payment = Convert.ToDecimal(payment);
            booking.checkIn = Convert.ToDateTime(fromDate);
            booking.checkOut= Convert.ToDateTime(toDate);


            var i = cust.Overlaps(booking);

            booking.RoomId = i.RoomId;
            booking.BookedOn = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                Customer customer = await db.Customers.FindAsync(booking.CustomerId);
                customer.Wallet = customer.Wallet - (long)Convert.ToDecimal(payment);
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Firstname", booking.CustomerId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomType", booking.RoomId);
            return View(booking);
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