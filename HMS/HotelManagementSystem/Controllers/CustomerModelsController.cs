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
    public class CustomerModelsController : Controller
    {
        private CustomerDB db = new CustomerDB();

        // GET: CustomerModels
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        // GET: CustomerModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = await db.Customers.FindAsync(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // GET: CustomerModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Cust_ID,Cust_Name,Password,ConfirmPassword,Cust_PhNo,Cust_Addr,Wallet")] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customerModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customerModel);
        }

        // GET: CustomerModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = await db.Customers.FindAsync(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: CustomerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Cust_ID,Cust_Name,Password,ConfirmPassword,Cust_PhNo,Cust_Addr,Wallet")] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerModel);
        }

        // GET: CustomerModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = await db.Customers.FindAsync(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: CustomerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomerModel customerModel = await db.Customers.FindAsync(id);
            db.Customers.Remove(customerModel);
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
