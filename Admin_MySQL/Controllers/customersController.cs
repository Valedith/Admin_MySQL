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
    public class customersController : Controller
    {
        private httt_dnEntities db = new httt_dnEntities();

        // GET: customers
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.UsernameSortParm = sortOrder == "username_asc" ? "username_desc" : "username_asc";
            ViewBag.SaltSortParm = sortOrder == "salt_asc" ? "salt_desc" : "salt_asc";
            ViewBag.PasswordSortParam = sortOrder == "password_asc" ? "password_desc" : "password_asc";
            ViewBag.EmailSortParam = sortOrder == "email_asc" ? "email_desc" : "email_asc";

            ViewBag.RegDateSortParam = sortOrder == "ngaydangky_asc" ? "ngaydangky_desc" : "ngaydangky_asc";
            ViewBag.TienAoSortParam = sortOrder == "tienao_asc" ? "tienao_desc" : "tienao_asc";
            ViewBag.isActivateSortParam = sortOrder == "tinhtrangkichhoat_asc" ? "tinhtrangkichhoat_desc" : "tinhtrangkichhoat_asc";
            ViewBag.NameIDSortParam = sortOrder == "hoten_asc" ? "hoten_desc" : "hoten_asc";
            ViewBag.NgaySinhIDSortParam = sortOrder == "ngaysinh_asc" ? "ngaysinh_desc" : "ngaysinh_asc";
            ViewBag.DienThoaiIDSortParam = sortOrder == "sodienthoai_asc" ? "sodienthoai_desc" : "sodienthoai_asc";

            ViewBag.GioiTinhSortParam = sortOrder == "gioitinh_asc" ? "gioitinh_desc" : "gioitinh_asc";
            ViewBag.DiaChiSortParam = sortOrder == "diachi_asc" ? "diachi_desc" : "diachi_asc";
            ViewBag.isMerchantIDSortParam = sortOrder == "ismerchant_asc" ? "ismerchant_desc" : "ismerchant_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var customer = from s in db.customers
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.hoten.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    customer = customer.OrderByDescending(s => s.customer_id);
                    break;
                case "username_desc":
                    customer = customer.OrderByDescending(s => s.username);
                    break;
                case "username_asc":
                    customer = customer.OrderBy(s => s.username);
                    break;
                case "salt_desc":
                    customer = customer.OrderByDescending(s => s.salt);
                    break;
                case "salt_asc":
                    customer = customer.OrderBy(s => s.salt);
                    break;
                case "password_desc":
                    customer = customer.OrderByDescending(s => s.password);
                    break;
                case "password_asc":
                    customer = customer.OrderBy(s => s.password);
                    break;
                case "email_desc":
                    customer = customer.OrderByDescending(s => s.email);
                    break;
                case "email_asc":
                    customer = customer.OrderBy(s => s.email);
                    break;

                case "ngaydangky_desc":
                    customer = customer.OrderByDescending(s => s.ngaydangky);
                    break;
                case "ngaydangky_asc":
                    customer = customer.OrderBy(s => s.ngaydangky);
                    break;


                case "tienao_desc":
                    customer = customer.OrderByDescending(s => s.tienao);
                    break;
                case "tienao_asc":
                    customer = customer.OrderBy(s => s.tienao);
                    break;


                case "tinhtrangkichhoat_desc":
                    customer = customer.OrderByDescending(s => s.tinhtrangkichhoat);
                    break;
                case "tinhtrangkichhoat_asc":
                    customer = customer.OrderBy(s => s.tinhtrangkichhoat);
                    break;


                case "hoten_desc":
                    customer = customer.OrderByDescending(s => s.hoten);
                    break;
                case "hoten_asc":
                    customer = customer.OrderBy(s => s.hoten);
                    break;


                case "sodienthoai_desc":
                    customer = customer.OrderByDescending(s => s.sodienthoai);
                    break;
                case "sodienthoai_asc":
                    customer = customer.OrderBy(s => s.sodienthoai);
                    break;

                case "ngaysinh_desc":
                    customer = customer.OrderByDescending(s => s.ngaysinh);
                    break;
                case "ngaysinh_asc":
                    customer = customer.OrderBy(s => s.ngaysinh);
                    break;

                case "gioitinh_desc":
                    customer = customer.OrderByDescending(s => s.gioitinh);
                    break;
                case "gioitinh_asc":
                    customer = customer.OrderBy(s => s.gioitinh);
                    break;

                case "diachi_desc":
                    customer = customer.OrderByDescending(s => s.diachi);
                    break;
                case "diachi_asc":
                    customer = customer.OrderBy(s => s.diachi);
                    break;

                case "ismerchant_desc":
                    customer = customer.OrderByDescending(s => s.ismechant);
                    break;
                case "ismerchant_asc":
                    customer = customer.OrderBy(s => s.ismechant);
                    break;


                default:
                    customer = customer.OrderBy(s => s.customer_id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(customer.ToPagedList(pageNumber, pageSize));

        }

        // GET: customers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_id,username,salt,password,email,ngaydangky,tienao,tinhtrangkichhoat,hoten,ngaysinh,sodienthoai,gioitinh,diachi,ismechant")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: customers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customer_id,username,salt,password,email,ngaydangky,tienao,tinhtrangkichhoat,hoten,ngaysinh,sodienthoai,gioitinh,diachi,ismechant")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: customers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
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
