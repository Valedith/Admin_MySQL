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
    public class sanphamsController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: sanphams
        /*
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "tensanpham" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var sanpham = from s in db.sanphams
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    sanpham = sanpham.OrderByDescending(s => s.tensanpham);
                    break;
                default:
                    sanpham = sanpham.OrderBy(s => s.tensanpham);
                    break;
            }
            return View(sanpham.ToList());
        }
        */
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.QuantitySortParm = sortOrder == "quantity_asc" ? "quantity_desc" : "quantity_asc";
            ViewBag.DescriptionSortParam = sortOrder == "description_asc" ? "description_desc" : "description_asc";
            ViewBag.PostDateSortParam = sortOrder == "postdate_asc" ? "postdate_desc" : "postdate_asc";


            ViewBag.isCheckedSortParam = sortOrder == "checked_asc" ? "checked_desc" : "checked_asc";
            ViewBag.isLockedSortParam = sortOrder == "locked_asc" ? "locked_desc" : "locked_asc";
            ViewBag.hangsanphamSortParam = sortOrder == "hangsanpham_asc" ? "hangsanpham_desc" : "hangsanpham_asc";
            ViewBag.loaisanphamSortParam = sortOrder == "loaisanpham_asc" ? "loaisanpham_desc" : "loaisanpham_asc";
            ViewBag.tinhtrangtonSortParam = sortOrder == "tinhtrangton_asc" ? "tinhtrangton_desc" : "tinhtrangton_asc";
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

            var sanpham = from s in db.sanphams
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(s => s.tensanpham.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    sanpham = sanpham.OrderByDescending(s => s.sanpham_id);
                    break;
                case "name_desc":
                    sanpham = sanpham.OrderByDescending(s => s.tensanpham);
                    break;
                case "name_asc":
                    sanpham = sanpham.OrderBy(s => s.tensanpham);
                    break;
                case "quantity_desc":
                    sanpham = sanpham.OrderByDescending(s => s.tongsoluongton);
                    break;
                case "quantity_asc":
                    sanpham = sanpham.OrderBy(s => s.tongsoluongton);
                    break;
                case "description_desc":
                    sanpham = sanpham.OrderByDescending(s => s.mota);
                    break;
                case "description_asc":
                    sanpham = sanpham.OrderBy(s => s.mota);
                    break;
                case "postdate_desc":
                    sanpham = sanpham.OrderByDescending(s => s.ngaydang);
                    break;
                case "postdate_asc":
                    sanpham = sanpham.OrderBy(s => s.ngaydang);
                    break;

                case "checked_desc":
                    sanpham = sanpham.OrderByDescending(s => s.tinhtrangduyet);
                    break;
                case "checked_asc":
                    sanpham = sanpham.OrderBy(s => s.tinhtrangduyet);
                    break;


                case "locked_desc":
                    sanpham = sanpham.OrderByDescending(s => s.tinhtrangkhoa);
                    break;
                case "locked_asc":
                    sanpham = sanpham.OrderBy(s => s.tinhtrangkhoa);
                    break;


                case "hangsanpham_desc":
                    sanpham = sanpham.OrderByDescending(s => s.hangsanpham.tenhangsanpham);
                    break;
                case "hangsanpham_asc":
                    sanpham = sanpham.OrderBy(s => s.hangsanpham.tenhangsanpham);
                    break;


                case "loaisanpham_desc":
                    sanpham = sanpham.OrderByDescending(s => s.loaisanpham.tenloaisanpham);
                    break;
                case "loaisanpham_asc":
                    sanpham = sanpham.OrderBy(s => s.loaisanpham.tenloaisanpham);
                    break;


                case "merchant_desc":
                    sanpham = sanpham.OrderByDescending(s => s.merchant.cmnd);
                    break;
                case "merchant_asc":
                    sanpham = sanpham.OrderBy(s => s.merchant.cmnd);
                    break;

                case "tinhtrangton_desc":
                    sanpham = sanpham.OrderByDescending(s => s.tinhtrangton.tentinhtrangton);
                    break;
                case "tinhtrangton_asc":
                    sanpham = sanpham.OrderBy(s => s.tinhtrangton.tentinhtrangton);
                    break;


                default:
                    sanpham = sanpham.OrderBy(s => s.sanpham_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sanpham.ToPagedList(pageNumber, pageSize));

        }
        // GET: sanphams/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: sanphams/Create
        public ActionResult Create()
        {
            ViewBag.hangsanpham_id = new SelectList(db.hangsanphams, "hangsanpham_id", "tenhangsanpham");
            ViewBag.loaisanpham_id = new SelectList(db.loaisanphams, "loaisanpham_id", "tenloaisanpham");
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd");
            ViewBag.tinhtrangton_id = new SelectList(db.tinhtrangtons, "tinhtrangton_id", "tentinhtrangton");
            return View();
        }

        // POST: sanphams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sanpham_id,tensanpham,tongsoluongton,mota,ngaydang,tinhtrangduyet,hinhanh,tinhtrangkhoa,hangsanpham_id,loaisanpham_id,tinhtrangton_id,merchant_id")] sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.sanphams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hangsanpham_id = new SelectList(db.hangsanphams, "hangsanpham_id", "tenhangsanpham", sanpham.hangsanpham_id);
            ViewBag.loaisanpham_id = new SelectList(db.loaisanphams, "loaisanpham_id", "tenloaisanpham", sanpham.loaisanpham_id);
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", sanpham.merchant_id);
            ViewBag.tinhtrangton_id = new SelectList(db.tinhtrangtons, "tinhtrangton_id", "tentinhtrangton", sanpham.tinhtrangton_id);
            return View(sanpham);
        }

        // GET: sanphams/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.hangsanpham_id = new SelectList(db.hangsanphams, "hangsanpham_id", "tenhangsanpham", sanpham.hangsanpham_id);
            ViewBag.loaisanpham_id = new SelectList(db.loaisanphams, "loaisanpham_id", "tenloaisanpham", sanpham.loaisanpham_id);
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", sanpham.merchant_id);
            ViewBag.tinhtrangton_id = new SelectList(db.tinhtrangtons, "tinhtrangton_id", "tentinhtrangton", sanpham.tinhtrangton_id);
            return View(sanpham);
        }

        // POST: sanphams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sanpham_id,tensanpham,tongsoluongton,mota,ngaydang,tinhtrangduyet,hinhanh,tinhtrangkhoa,hangsanpham_id,loaisanpham_id,tinhtrangton_id,merchant_id")] sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hangsanpham_id = new SelectList(db.hangsanphams, "hangsanpham_id", "tenhangsanpham", sanpham.hangsanpham_id);
            ViewBag.loaisanpham_id = new SelectList(db.loaisanphams, "loaisanpham_id", "tenloaisanpham", sanpham.loaisanpham_id);
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", sanpham.merchant_id);
            ViewBag.tinhtrangton_id = new SelectList(db.tinhtrangtons, "tinhtrangton_id", "tentinhtrangton", sanpham.tinhtrangton_id);
            return View(sanpham);
        }

        // GET: sanphams/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: sanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            sanpham sanpham = db.sanphams.Find(id);
            db.sanphams.Remove(sanpham);
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
