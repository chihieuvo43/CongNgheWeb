using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;

namespace CongNgheWeb_3_0.Controllers
{
    public class MenuController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        // GET: Menu
        //Show list menu Parent
        public ActionResult Index()
        {
            return View(db.tbl_Menu.Where(x=>x.ParentID==null).ToList());
        }
        //Edit menu Parent
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Menu = db.tbl_Menu.Find(id);
            return View(Menu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Menu Menu)
        {
            if(ModelState.IsValid)
            {
                db.Entry(Menu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }
        //Craete Menu Parent
        [HttpGet]
        public ActionResult CreateMenuParent()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateMenuParent(tbl_Menu Menu)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Menu.Add(Menu);
                db.SaveChanges();
                return RedirectToAction("Index", "Menu");
            }
            ViewBag.Status = "Không tạo được danh mục";
            return View();
        }

        //Show menu child
        //id is menu parent
        public ActionResult MenuChild(int id)
        {
            var Menu = db.tbl_Menu.SingleOrDefault(x => x.ID == id).MenuName;
            ViewBag.MenuParent = Menu;
            return View(db.tbl_Menu.Where(x => x.ParentID == id).ToList());
        }
        //Create menu child
        //id is menu parent
        public ActionResult CreateMenuChild(int id)
        {
            var Menu = db.tbl_Menu.SingleOrDefault(x => x.ID == id).MenuName;
            ViewBag.MenuParent = Menu;
            return View(db.tbl_Menu.Where(x => x.ParentID == id).ToList());
        }

    }
}