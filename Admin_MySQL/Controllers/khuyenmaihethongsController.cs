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
    public class khuyenmaihethongsController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: khuyenmaihethongs
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.NgayBDSortParm = sortOrder == "ngaybatdau_asc" ? "ngaybatdau_desc" : "ngaybatdau_asc";
            ViewBag.NgayKTSortParam = sortOrder == "ngayketthuc_asc" ? "ngayketthuc_desc" : "ngayketthuc_asc";
            ViewBag.MucKhuyenMaiToiDaSortParam = sortOrder == "muckhuyenmaihethongtoida_asc" ? "muckhuyenmaihethongtoida_desc" : "muckhuyenmaihethongtoida_asc";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var khuyenmaihethong = from s in db.khuyenmaihethongs
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                khuyenmaihethong = khuyenmaihethong.Where(s => s.tenkhuyenmaihethong.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    khuyenmaihethong = khuyenmaihethong.OrderByDescending(s => s.khuyenmaihethong_id);
                    break;
                case "name_desc":
                    khuyenmaihethong = khuyenmaihethong.OrderByDescending(s => s.tenkhuyenmaihethong);
                    break;
                case "name_asc":
                    khuyenmaihethong = khuyenmaihethong.OrderBy(s => s.tenkhuyenmaihethong);
                    break;
                case "ngaybatdau_desc":
                    khuyenmaihethong = khuyenmaihethong.OrderByDescending(s => s.ngaybatdau);
                    break;
                case "ngaybatdau_asc":
                    khuyenmaihethong = khuyenmaihethong.OrderBy(s => s.ngaybatdau);
                    break;
                case "ngayketthuc_desc":
                    khuyenmaihethong = khuyenmaihethong.OrderByDescending(s => s.ngayketthuc);
                    break;
                case "ngayketthuc_asc":
                    khuyenmaihethong = khuyenmaihethong.OrderBy(s => s.ngayketthuc);
                    break;
                case "muckhuyenmaihethongtoida_desc":
                    khuyenmaihethong = khuyenmaihethong.OrderByDescending(s => s.muckhuyenmaitoida);
                    break;
                case "muckhuyenmaihethongtoida_asc":
                    khuyenmaihethong = khuyenmaihethong.OrderBy(s => s.muckhuyenmaitoida);
                    break;
                    
                default:
                    khuyenmaihethong = khuyenmaihethong.OrderBy(s => s.khuyenmaihethong_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(khuyenmaihethong.ToPagedList(pageNumber, pageSize));

        }

        // GET: khuyenmaihethongs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khuyenmaihethong khuyenmaihethong = db.khuyenmaihethongs.Find(id);
            if (khuyenmaihethong == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmaihethong);
        }

        // GET: khuyenmaihethongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: khuyenmaihethongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "khuyenmaihethong_id,tenkhuyenmaihethong,ngaybatdau,ngayketthuc,muckhuyenmaihethongtoida")] khuyenmaihethong khuyenmaihethong)
        {
            if (ModelState.IsValid)
            {
                db.khuyenmaihethongs.Add(khuyenmaihethong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khuyenmaihethong);
        }

        // GET: khuyenmaihethongs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khuyenmaihethong khuyenmaihethong = db.khuyenmaihethongs.Find(id);
            if (khuyenmaihethong == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmaihethong);
        }

        // POST: khuyenmaihethongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "khuyenmaihethong_id,tenkhuyenmaihethong,ngaybatdau,ngayketthuc,muckhuyenmaihethongtoida")] khuyenmaihethong khuyenmaihethong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khuyenmaihethong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khuyenmaihethong);
        }

        // GET: khuyenmaihethongs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khuyenmaihethong khuyenmaihethong = db.khuyenmaihethongs.Find(id);
            if (khuyenmaihethong == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmaihethong);
        }

        // POST: khuyenmaihethongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            khuyenmaihethong khuyenmaihethong = db.khuyenmaihethongs.Find(id);
            db.khuyenmaihethongs.Remove(khuyenmaihethong);
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
