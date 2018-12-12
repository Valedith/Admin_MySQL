using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin_MySQL.Models;
using PagedList;

namespace Admin_MySQL.Controllers
{
    public class merchantsController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: merchants
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.CMNDSortParm = sortOrder == "cmnd_asc" ? "cmnd_desc" : "cmnd_asc";
            ViewBag.TenShopSortParm = sortOrder == "tenshop_asc" ? "tenshop_desc" : "tenshop_asc";
            ViewBag.AnhShopSortParam = sortOrder == "anhshop_asc" ? "anhshop_desc" : "anhshop_asc";
            ViewBag.RatingSort = sortOrder == "diemdanhgia_asc" ? "diemdanhgia_desc" : "diemdanhgia_asc";


            ViewBag.NgayDangKytParam = sortOrder == "ngaydangky_asc" ? "ngaydangky_desc" : "ngaydangky_asc";
            ViewBag.TaiKhoanPayPalParam = sortOrder == "tkpaypal_asc" ? "tkpaypal_desc" : "tkpaypal_asc";
            ViewBag.CapDoIDSortParam = sortOrder == "capdoid_asc" ? "capdoid_desc" : "capdoid_asc";
            ViewBag.customerIDSortParam = sortOrder == "customer_asc" ? "customer_desc" : "customer_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var merchant = from s in db.merchants
                          select s;
            //ten merchant
            if (!String.IsNullOrEmpty(searchString))
            {
                merchant = merchant.Where(s => s.customer.hoten.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    merchant = merchant.OrderByDescending(s => s.merchant_id);
                    break;
                case "cmnd_desc":
                    merchant = merchant.OrderByDescending(s => s.cmnd);
                    break;
                case "cmnd_asc":
                    merchant = merchant.OrderBy(s => s.cmnd);
                    break;
                case "tenshop_desc":
                    merchant = merchant.OrderByDescending(s => s.tenshop);
                    break;
                case "tenshop_asc":
                    merchant = merchant.OrderBy(s => s.tenshop);
                    break;
                case "anhshop_desc":
                    merchant = merchant.OrderByDescending(s => s.anhshop);
                    break;
                case "anhshop_asc":
                    merchant = merchant.OrderBy(s => s.anhshop);
                    break;
                case "diemdanhgia_desc":
                    merchant = merchant.OrderByDescending(s => s.diemdanhgia);
                    break;
                case "diemdanhgia_asc":
                    merchant = merchant.OrderBy(s => s.diemdanhgia);
                    break;

                case "ngaydangky_desc":
                    merchant = merchant.OrderByDescending(s => s.ngaydangky);
                    break;
                case "ngaydangky_asc":
                    merchant = merchant.OrderBy(s => s.ngaydangky);
                    break;


                case "tkpaypal_desc":
                    merchant = merchant.OrderByDescending(s => s.taikhoanpaypal);
                    break;
                case "tkpaypal_asc":
                    merchant = merchant.OrderBy(s => s.taikhoanpaypal);
                    break;


                case "capdo_desc":
                    merchant = merchant.OrderByDescending(s => s.capdo.tencapdo);
                    break;
                case "capdo_asc":
                    merchant = merchant.OrderBy(s => s.capdo.tencapdo);
                    break;


                case "customer_desc":
                    merchant = merchant.OrderByDescending(s => s.customer.hoten);
                    break;
                case "customer_asc":
                    merchant = merchant.OrderBy(s => s.customer.hoten);
                    break;
                    
                default:
                    merchant = merchant.OrderBy(s => s.merchant_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(merchant.ToPagedList(pageNumber, pageSize));

        }

        // GET: merchants/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            merchant merchant = db.merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // GET: merchants/Create
        public ActionResult Create()
        {
            ViewBag.capdo_id = new SelectList(db.capdoes, "capdo_id", "tencapdo");
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username");
            return View();
        }

        // POST: merchants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "merchant_id,cmnd,tenshop,anhshop,diemdanhgia,ngaydangky,taikhoanpaypal,capdo_id,customer_id")] merchant merchant)
        {
            if (ModelState.IsValid)
            {
                db.merchants.Add(merchant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.capdo_id = new SelectList(db.capdoes, "capdo_id", "tencapdo", merchant.capdo_id);
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username", merchant.customer_id);
            return View(merchant);
        }

        // GET: merchants/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            merchant merchant = db.merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            ViewBag.capdo_id = new SelectList(db.capdoes, "capdo_id", "tencapdo", merchant.capdo_id);
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username", merchant.customer_id);
            return View(merchant);
        }

        // POST: merchants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "merchant_id,cmnd,tenshop,anhshop,diemdanhgia,ngaydangky,taikhoanpaypal,capdo_id,customer_id")] merchant merchant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.capdo_id = new SelectList(db.capdoes, "capdo_id", "tencapdo", merchant.capdo_id);
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username", merchant.customer_id);
            return View(merchant);
        }

        // GET: merchants/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            merchant merchant = db.merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // POST: merchants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            merchant merchant = db.merchants.Find(id);
            db.merchants.Remove(merchant);
            db.SaveChanges();
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
