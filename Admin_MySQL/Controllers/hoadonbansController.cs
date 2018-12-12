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
    public class hoadonbansController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: hoadonbans
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NgayTaoSortParm = sortOrder == "ngaytao_asc" ? "ngaytao_desc" : "ngaytao_asc";
            ViewBag.NgayHTSortParm = sortOrder == "ngayhoanthanh_asc" ? "ngayhoanthanh_desc" : "ngayhoanthanh_asc";
            ViewBag.NoiGuiSortParam = sortOrder == "noigui_asc" ? "noigui_desc" : "noigui_asc";
            ViewBag.SoLuongSortParam = sortOrder == "soluong_asc" ? "soluong_desc" : "soluong_asc";


            ViewBag.HTThanhToanSortParam = sortOrder == "hinhthucthanhtoan_asc" ? "hinhthucthanhtoan_desc" : "hinhthucthanhtoan_asc";
            ViewBag.SanPhamSortParam = sortOrder == "sanpham_asc" ? "sanpham_desc" : "sanpham_asc";
            ViewBag.MerchantSortParam = sortOrder == "merchant_asc" ? "merchant_desc" : "merchant_asc";
            ViewBag.TinhTrangGiaoHangSortParam = sortOrder == "tinhtranggiaohang_asc" ? "tinhtranggiaohang_desc" : "tinhtranggiaohang_asc";
            ViewBag.HoaDonMuaSortParam = sortOrder == "hoadonmua_asc" ? "hoadonmua_desc" : "hoadonmua_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var hoadonban = from s in db.hoadonbans
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                hoadonban = hoadonban.Where(s => s.merchant.customer.hoten.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.hoadonban_id);
                    break;
                case "ngaytao_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.ngaytao);
                    break;
                case "ngaytao_asc":
                    hoadonban = hoadonban.OrderBy(s => s.ngaytao);
                    break;
                case "ngayhoanthanh_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.ngayhoanthanh);
                    break;
                case "ngayhoanthanh_asc":
                    hoadonban = hoadonban.OrderBy(s => s.ngayhoanthanh);
                    break;
                case "noigui_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.noigui);
                    break;
                case "noigui_asc":
                    hoadonban = hoadonban.OrderBy(s => s.noigui);
                    break;
                case "soluong_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.soluong);
                    break;
                case "soluong_asc":
                    hoadonban = hoadonban.OrderBy(s => s.soluong);
                    break;

                case "hinhthucthanhtoan_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.hinhthucthanhtoan.tenhinhthucthanhtoan);
                    break;
                case "hinhthucthanhtoan_asc":
                    hoadonban = hoadonban.OrderBy(s => s.hinhthucthanhtoan.tenhinhthucthanhtoan);
                    break;


                case "sanpham_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.sanpham.tensanpham);
                    break;
                case "sanpham_asc":
                    hoadonban = hoadonban.OrderBy(s => s.sanpham.tensanpham);
                    break;


                case "merchant_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.merchant.customer.hoten);
                    break;
                case "merchant_asc":
                    hoadonban = hoadonban.OrderBy(s => s.merchant.customer.hoten);
                    break;


                case "tinhtranggiaohang_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.tinhtranggiaohang.tentinhtranggiaohang);
                    break;
                case "tinhtranggiaohang_asc":
                    hoadonban = hoadonban.OrderBy(s => s.tinhtranggiaohang.tentinhtranggiaohang);
                    break;                    

                case "hoadonmua_desc":
                    hoadonban = hoadonban.OrderByDescending(s => s.hoadonmua.customer.hoten);
                    break;
                case "hoadonmua_asc":
                    hoadonban = hoadonban.OrderBy(s => s.hoadonmua.customer.hoten);
                    break;


                default:
                    hoadonban = hoadonban.OrderBy(s => s.hoadonban_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(hoadonban.ToPagedList(pageNumber, pageSize));

        }

        // GET: hoadonbans/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonban hoadonban = db.hoadonbans.Find(id);
            if (hoadonban == null)
            {
                return HttpNotFound();
            }
            return View(hoadonban);
        }

        // GET: hoadonbans/Create
        public ActionResult Create()
        {
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan");
            ViewBag.hoadonban_id = new SelectList(db.hoadonbans, "hoadonban_id", "tenhoadonban");
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd");
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang");
            ViewBag.hoadonmua_id = new SelectList(db.hoadonmuas, "hoadonmua_id", "noinhan");
            return View();
        }

        // POST: hoadonbans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hoadonban_id,ngaytao,ngayhoanthanh,noigui,soluong,hoadonban_id,merchant_id,hinhthucthanhtoan_id,tinhtranggiaohang_id,hoadonmua_id")] hoadonban hoadonban)
        {
            if (ModelState.IsValid)
            {
                db.hoadonbans.Add(hoadonban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan", hoadonban.hinhthucthanhtoan_id);
            ViewBag.hoadonban_id = new SelectList(db.hoadonbans, "hoadonban_id", "tenhoadonban", hoadonban.hoadonban_id);
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", hoadonban.merchant_id);
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang", hoadonban.tinhtranggiaohang_id);
            ViewBag.hoadonmua_id = new SelectList(db.hoadonmuas, "hoadonmua_id", "noinhan", hoadonban.hoadonmua_id);
            return View(hoadonban);
        }

        // GET: hoadonbans/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonban hoadonban = db.hoadonbans.Find(id);
            if (hoadonban == null)
            {
                return HttpNotFound();
            }
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan", hoadonban.hinhthucthanhtoan_id);
            ViewBag.hoadonban_id = new SelectList(db.hoadonbans, "hoadonban_id", "tenhoadonban", hoadonban.hoadonban_id);
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", hoadonban.merchant_id);
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang", hoadonban.tinhtranggiaohang_id);
            ViewBag.hoadonmua_id = new SelectList(db.hoadonmuas, "hoadonmua_id", "noinhan", hoadonban.hoadonmua_id);
            return View(hoadonban);
        }

        // POST: hoadonbans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hoadonban_id,ngaytao,ngayhoanthanh,noigui,soluong,hoadonban_id,merchant_id,hinhthucthanhtoan_id,tinhtranggiaohang_id,hoadonmua_id")] hoadonban hoadonban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoadonban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan", hoadonban.hinhthucthanhtoan_id);
            ViewBag.hoadonban_id = new SelectList(db.hoadonbans, "hoadonban_id", "tenhoadonban", hoadonban.hoadonban_id);
            ViewBag.merchant_id = new SelectList(db.merchants, "merchant_id", "cmnd", hoadonban.merchant_id);
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang", hoadonban.tinhtranggiaohang_id);
            ViewBag.hoadonmua_id = new SelectList(db.hoadonmuas, "hoadonmua_id", "noinhan", hoadonban.hoadonmua_id);
            return View(hoadonban);
        }

        // GET: hoadonbans/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonban hoadonban = db.hoadonbans.Find(id);
            if (hoadonban == null)
            {
                return HttpNotFound();
            }
            return View(hoadonban);
        }

        // POST: hoadonbans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            hoadonban hoadonban = db.hoadonbans.Find(id);
            db.hoadonbans.Remove(hoadonban);
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
