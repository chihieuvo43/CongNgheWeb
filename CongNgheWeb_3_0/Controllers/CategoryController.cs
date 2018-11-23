using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;

namespace CongNgheWeb_3_0.Controllers
{
    public class CategoryController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.tbl_Category.ToList());
        }
        
        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbl_Category category)
        {
            try
            {
                var categoryInData = db.tbl_Category.SingleOrDefault(x => x.CategoryName == category.CategoryName);
                if(categoryInData!=null)
                {
                    ViewBag.Status = "Danh mục này đã tồn tại";
                    return View();
                }
                if(ModelState.IsValid)
                {
                    category.Deleted = false;
                    db.tbl_Category.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index","Category");
                }
                ViewBag.Status = "Không thêm được danh mục";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Status = "Không thêm được danh mục"+ex;
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var Category = db.tbl_Category.SingleOrDefault(x => x.ID == id);
            if(Category==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                 return View(Category);
            }
           
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Category category)
        {  
            if(ModelState.IsValid)
            {
                var CategoryInData = db.tbl_Category.SingleOrDefault(x=>x.CategoryName==category.CategoryName && x.ID!=category.ID);
                if (CategoryInData != null)
                {
                    ViewBag.Status = "Danh mục " + category.CategoryName + " đã tồn tại";
                    return View(category);
                }
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ViewBag.Status = "Không sửa được danh mục";
                return View(category);
            }
       
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var CategoryInData = db.tbl_Category.SingleOrDefault(x => x.ID == id);
            if(CategoryInData==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(CategoryInData);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var CategoryInData = db.tbl_Category.SingleOrDefault(x => x.ID == id);
            if (CategoryInData == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.tbl_Category.Remove(CategoryInData);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
        }
        //Partil view for list Category

        public PartialViewResult PartilViewListCategory()
        {
            var ListCategory = db.tbl_Category.ToList();
            return PartialView(ListCategory);
        }

        //----------------------Sort course-----------------------------------
        public ActionResult ShowSortCourse(int id)
        {
            
            ViewBag.CourseID = id;
            ViewBag.CourseName = db.tbl_Category.SingleOrDefault(x => x.ID == id).CategoryName;
            return View(db.tbl_Sort_Course.Where(x => x.CategoryID == id && x.Deleted == false).ToList());
        }

        [HttpGet]
        public ActionResult CreateSortCourse(int id)
        {
            ViewBag.CourseID = id;
            ViewBag.CourseName = db.tbl_Category.SingleOrDefault(x => x.ID == id).CategoryName;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSortCourse(int id,tbl_Sort_Course sortCourse)
        {
            tbl_Sort_Course SortCourse = new tbl_Sort_Course();
            ViewBag.CourseID = id;
            ViewBag.CourseName = db.tbl_Category.SingleOrDefault(x => x.ID == id).CategoryName;
            var SortNameInData = db.tbl_Sort_Course.Where(x=>x.SortCourseName==sortCourse.SortCourseName).FirstOrDefault();
            //var SortNameInData = db.tbl_Sort_Course.SingleOrDefault(x=>x.SortCourseName==sortCourse.SortCourseName);
            if(SortNameInData!=null)
            {
                SortNameInData.ID = sortCourse.ID;
                SortCourse.Deleted = true;
                db.Entry(SortNameInData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowSortCourse", "Category", new { @id = SortCourse.tbl_Category.ID });
            }

            if(ModelState.IsValid)
            {
                //tbl_Sort_Course SortCourse = new tbl_Sort_Course();
           
                SortCourse.CategoryID = id;
                SortCourse.Deleted = false;
                SortCourse.SortCourseName = sortCourse.SortCourseName;
                db.tbl_Sort_Course.Add(SortCourse);
                db.SaveChanges();
                return RedirectToAction("ShowSortCourse", "Category", new { @id = id });
            }
            ViewBag.Status = "Không thêm được danh mục!";
            return View();
        }

        [HttpGet]
        public ActionResult DeleteSortCourse(int id)
        {
            //ViewBag.CourseID = id;
            //ViewBag.CourseName = db.tbl_Category.SingleOrDefault(x => x.ID == id).CategoryName;
            var SortCousre = db.tbl_Sort_Course.SingleOrDefault(x => x.ID == id);
            if(SortCousre==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(SortCousre);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteSortCourse(tbl_Sort_Course sortCourse)
        {
            //ViewBag.CourseID = sortCourse.tbl_Category.ID;
            //ViewBag.CourseName = db.tbl_Category.SingleOrDefault(x => x.ID == id).CategoryName;
            //var SortCousre = db.tbl_Sort_Course.SingleOrDefault(x => x.ID == id);
            //if (SortCousre == null)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            var SortCourse = db.tbl_Sort_Course.Find(sortCourse.ID);
            if (SortCourse == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SortCourse.Deleted = true;
            db.Entry(SortCourse).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ShowSortCourse", "Category", new { @id = SortCourse.tbl_Category.ID });
        }

    }
}
