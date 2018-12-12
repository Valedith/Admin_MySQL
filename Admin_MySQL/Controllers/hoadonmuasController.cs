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
    public class hoadonmuasController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: hoadonmuas
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NoiNhanSortParm = sortOrder == "noinhan_asc" ? "noinhan_desc" : "noinhan_asc";
            ViewBag.SoSanPhamSortParm = sortOrder == "sosanpham_asc" ? "sosanpham_desc" : "sosanpham_asc";
            ViewBag.NgayTaoSortParm = sortOrder == "ngaytao_asc" ? "ngaytao_desc" : "ngaytao_asc";
            ViewBag.TongTienSortParm = sortOrder == "tongtien_asc" ? "tongtien_desc" : "tongtien_asc";
            ViewBag.NameIDSortParam = sortOrder == "hoten_asc" ? "hoten_desc" : "hoten_asc";

            ViewBag.DienThoaiIDSortParam = sortOrder == "sodienthoai_asc" ? "sodienthoai_desc" : "sodienthoai_asc";
            ViewBag.EmailSortParam = sortOrder == "email_asc" ? "email_desc" : "email_asc";

            ViewBag.HinhThucThanhToanSortParam = sortOrder == "hinhthucthanhtoan_asc" ? "hinhthucthanhtoan_desc" : "hinhthucthanhtoan_asc";
            ViewBag.TinhTrangGiaoHangSortParam = sortOrder == "tinhtranggiaohang_asc" ? "tinhtranggiaohang_desc" : "tinhtranggiaohang_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var hoadonmua = from s in db.hoadonmuas
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                hoadonmua = hoadonmua.Where(s => s.hoten.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.hoadonmua_id);
                    break;
                case "noinhan_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.noinhan);
                    break;
                case "noinhan_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.noinhan);
                    break;
                case "sosanpham_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.sosanpham);
                    break;
                case "sosanpham_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.sosanpham);
                    break;
                case "ngaytao_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.ngaytao);
                    break;
                case "ngaytao_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.ngaytao);
                    break;
                case "tongtien_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.tongtien);
                    break;
                case "tongtien_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.tongtien);
                    break;

                case "hoten_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.hoten);
                    break;
                case "hoten_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.hoten);
                    break;


                case "sanpham_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.sosanpham);
                    break;
                case "sanpham_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.sosanpham);
                    break;


                case "sodienthoai_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.sodienthoai);
                    break;
                case "sodienthoai_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.sodienthoai);
                    break;


                case "email_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.email);
                    break;
                case "email_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.email);
                    break;


                case "hinhthucthanhtoan_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.hinhthucthanhtoan.tenhinhthucthanhtoan);
                    break;
                case "hinhthucthanhtoan_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.hinhthucthanhtoan.tenhinhthucthanhtoan);
                    break;


                case "tinhtranggiaohang_desc":
                    hoadonmua = hoadonmua.OrderByDescending(s => s.tinhtranggiaohang.tentinhtranggiaohang);
                    break;
                case "tinhtranggiaohang_asc":
                    hoadonmua = hoadonmua.OrderBy(s => s.tinhtranggiaohang.tentinhtranggiaohang);
                    break;

                default:
                    hoadonmua = hoadonmua.OrderBy(s => s.hoadonmua_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(hoadonmua.ToPagedList(pageNumber, pageSize));

        }

        // GET: hoadonmuas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonmua hoadonmua = db.hoadonmuas.Find(id);
            if (hoadonmua == null)
            {
                return HttpNotFound();
            }
            return View(hoadonmua);
        }

        // GET: hoadonmuas/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username");
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan");
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang");
            return View();
        }

        // POST: hoadonmuas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hoadonmua_id,noinhan,sosanpham,ngaytao,tongtien,customer_id,hinhthucthanhtoan_id,tinhtranggiaohang_id,hoten,sodienthoai,email")] hoadonmua hoadonmua)
        {
            if (ModelState.IsValid)
            {
                db.hoadonmuas.Add(hoadonmua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username", hoadonmua.customer_id);
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan", hoadonmua.hinhthucthanhtoan_id);
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang", hoadonmua.tinhtranggiaohang_id);
            return View(hoadonmua);
        }

        // GET: hoadonmuas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonmua hoadonmua = db.hoadonmuas.Find(id);
            if (hoadonmua == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username", hoadonmua.customer_id);
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan", hoadonmua.hinhthucthanhtoan_id);
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang", hoadonmua.tinhtranggiaohang_id);
            return View(hoadonmua);
        }

        // POST: hoadonmuas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hoadonmua_id,noinhan,sosanpham,ngaytao,tongtien,customer_id,hinhthucthanhtoan_id,tinhtranggiaohang_id,hoten,sodienthoai,email")] hoadonmua hoadonmua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoadonmua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "username", hoadonmua.customer_id);
            ViewBag.hinhthucthanhtoan_id = new SelectList(db.hinhthucthanhtoans, "hinhthucthanhtoan_id", "tenhinhthucthanhtoan", hoadonmua.hinhthucthanhtoan_id);
            ViewBag.tinhtranggiaohang_id = new SelectList(db.tinhtranggiaohangs, "tinhtranggiaohang_id", "tentinhtranggiaohang", hoadonmua.tinhtranggiaohang_id);
            return View(hoadonmua);
        }

        // GET: hoadonmuas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonmua hoadonmua = db.hoadonmuas.Find(id);
            if (hoadonmua == null)
            {
                return HttpNotFound();
            }
            return View(hoadonmua);
        }

        // POST: hoadonmuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            hoadonmua hoadonmua = db.hoadonmuas.Find(id);
            db.hoadonmuas.Remove(hoadonmua);
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
