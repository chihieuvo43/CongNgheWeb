using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;
using PagedList.Mvc;
using PagedList;

namespace CongNgheWeb_3_0.Controllers
{
    public class PosterController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();

        // GET: Slide
        public ActionResult Index(int? page)
        {
            int pageSize =3;
            int pageNumber = (page ?? 1);
            return View(db.tbl_Poster.ToList().OrderByDescending(x=>x.CreateDate).ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Show()
        {
            return PartialView(db.tbl_Poster.Where(x => x.Clock == false).OrderByDescending(x => x.CreateDate).Take(1));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Create(tbl_Poster Poster,HttpPostedFileBase PosterImage)
        {

            if (PosterImage == null)
            {
                ViewBag.Status = "Chọn ảnh cho Poster";
                return View();
            }
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(PosterImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Posters"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Status = "Ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    PosterImage.SaveAs(path);
                }
                Poster.ImagePoster = PosterImage.FileName;
                Poster.CreateDate = DateTime.Now;
                Poster.Clock = true;
                db.tbl_Poster.Add(Poster);
                db.SaveChanges();
                return RedirectToAction("Index", "Poster");
            }
            else
            {

                ViewBag.Status = "Không thêm được ảnh vào kho";
                return View();
            }
        }

        public ActionResult Clock(int id)
        {
            var Poster = db.tbl_Poster.SingleOrDefault(x => x.ID == id);
            if (Poster == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (Poster.Clock == true)
            {
                db.Entry(Poster).State = System.Data.Entity.EntityState.Modified;
                Poster.Clock = false;
                db.SaveChanges();
            }
            else
            {
                db.Entry(Poster).State = System.Data.Entity.EntityState.Modified;
                Poster.Clock = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Poster");
        }


        public ActionResult Delete(int id)
        {
            var PosterInData = db.tbl_Poster.SingleOrDefault(x => x.ID == id);
            if (PosterInData == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                //delete image file
                var filePath = Path.Combine(Server.MapPath("~/Images/Posters/" + PosterInData.ImagePoster));
                //var filePath = Server.MapPath("~/Images/Slides/" + SlideInData.SildeImage);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                else
                {
                    ViewBag.Status = "Không xóa được ảnh";
                }
                db.tbl_Poster.Remove(PosterInData);
                db.SaveChanges();
                return RedirectToAction("Index", "Poster");
            }
        }
        public ActionResult ShowInHomePage()
        {
            return View(db.tbl_Poster.Where(x => x.Clock == false).Take(1));
        }
    }
}