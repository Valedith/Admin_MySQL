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
    public class khuyenmaisController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: khuyenmais
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.NgayBDSortParm = sortOrder == "ngaybatdau_asc" ? "ngaybatdau_desc" : "ngaybatdau_asc";
            ViewBag.NgayKTSortParam = sortOrder == "ngayketthuc_asc" ? "ngayketthuc_desc" : "ngayketthuc_asc";
            ViewBag.MucKhuyenMaiToiDaSortParam = sortOrder == "muckhuyenmaitoida_asc" ? "muckhuyenmaitoida_desc" : "muckhuyenmaitoida_asc";

            ViewBag.merchantSortParam = sortOrder == "merchant_asc" ? "merchant_desc" : "merchant_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var khuyenmai = from s in db.khuyenmais
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                khuyenmai = khuyenmai.Where(s => s.tenkhuyenmai.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    khuyenmai = khuyenmai.OrderByDescending(s => s.khuyenmai_id);
                    break;
                case "name_desc":
                    khuyenmai = khuyenmai.OrderByDescending(s => s.tenkhuyenmai);
                    break;
                case "name_asc":
                    khuyenmai = khuyenmai.OrderBy(s => s.tenkhuyenmai);
                    break;
                case "ngaybatdau_desc":
                    khuyenmai = khuyenmai.OrderByDescending(s => s.ngaybatdau);
                    break;
                case "ngaybatdau_asc":
                    khuyenmai = khuyenmai.OrderBy(s => s.ngaybatdau);
                    break;
                case "ngayketthuc_desc":
                    khuyenmai = khuyenmai.OrderByDescending(s => s.ngayketthuc);
                    break;
                case "ngayketthuc_asc":
                    khuyenmai = khuyenmai.OrderBy(s => s.ngayketthuc);
                    break;
                case "muckhuyenmaitoida_desc":
                    khuyenmai = khuyenmai.OrderByDescending(s => s.muckhuyenmaitoida);
                    break;
                case "muckhuyenmaitoida_asc":
                    khuyenmai = khuyenmai.OrderBy(s => s.muckhuyenmaitoida);
                    break;
                    
                case "merchant_desc":
                    khuyenmai = khuyenmai.OrderByDescending(s => s.merchant.customer.hoten);
                    break;
                case "merchant_asc":
                    khuyenmai = khuyenmai.OrderBy(s => s.merchant.customer.hoten);
                    break;
                    
                default:
                    khuyenmai = khuyenmai.OrderBy(s => s.khuyenmai_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(khuyenmai.ToPagedList(pageNumber, pageSize));

        }

        // GET: khuyenmais/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khuyenmai khuyenmai = db.khuyenmais.Find(id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmai);
        }

        // GET: khuyenmais/Create
        public ActionResult Create()
        {
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd");
            return View();
        }

        // POST: khuyenmais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "khuyenmai_id,tenkhuyenmai,ngaybatdau,ngayketthuc,muckhuyenmaitoida,merchant_id")] khuyenmai khuyenmai)
        {
            if (ModelState.IsValid)
            {
                db.khuyenmais.Add(khuyenmai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", khuyenmai.merchant_id);
            return View(khuyenmai);
        }

        // GET: khuyenmais/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khuyenmai khuyenmai = db.khuyenmais.Find(id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", khuyenmai.merchant_id);
            return View(khuyenmai);
        }

        // POST: khuyenmais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "khuyenmai_id,tenkhuyenmai,ngaybatdau,ngayketthuc,muckhuyenmaitoida,merchant_id")] khuyenmai khuyenmai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khuyenmai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", khuyenmai.merchant_id);
            return View(khuyenmai);
        }

        // GET: khuyenmais/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khuyenmai khuyenmai = db.khuyenmais.Find(id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmai);
        }

        // POST: khuyenmais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            khuyenmai khuyenmai = db.khuyenmais.Find(id);
            db.khuyenmais.Remove(khuyenmai);
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
