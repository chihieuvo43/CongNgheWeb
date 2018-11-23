using System;
using Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Configuration;

namespace CongNgheWeb_3_0.Controllers
{
    public class UserController : Controller
    {
        E_ClassEntities db = new E_ClassEntities()
;        // GET: User
        public ActionResult Index()
        {
            return View();
        }
      
        public static string GenerateRandomPassword(int length)
        {
            string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            string allowedNumberChars = "0123456789";
            char[] chars = new char[length];
            Random rd = new Random();
            bool useLetter = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetter)
                {
                    chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                    useLetter = false;
                }
                else
                {
                    chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                    useLetter = true;
                }
            }
            return new string(chars);
        }

        //Get Create teacher

        public ActionResult CreateTeacher()
        {
            return View();
        }


        //Post Create teacher
        //[HttpPost,ActionName("CreateTeacher")]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateUserDontSendMail(tbl_User user)
        {
            string CreateRandomPassword = GenerateRandomPassword(6);
            var EmailInData = db.tbl_User.SingleOrDefault(x => x.Email == user.Email);
            if(EmailInData != null)
            {
                ViewBag.Status = "Email đã tồn tại. Xin hãy nhập Email khác!";
                return View("CreateTeacher");
            }
            else 
            {
                    tbl_User User = new tbl_User();
                    User.Clock = false;
                    User.CreateDate = DateTime.Now;
                    User.DescriptionUser = null;
                    User.LoginName = user.Email;
                    User.NumberPhone = null;
                    User.UserImage = "no-image.jpg";
                    User.Position = 3;
                    User.SendMail = false;
                    User.Notificationed = false;
                    User.Email = user.Email;
                    User.NumberPhone = null;
                    User.PasswordUser =Convert.ToString(HashPassword.Hash(CreateRandomPassword));
                    User.UserName = user.UserName;
                    db.tbl_User.Add(User);
                    db.SaveChanges();
                    return RedirectToAction("ListTeacher","Admin");

            }
          
            ViewBag.Status = "Không tạo được tài khoản";
            return View("CreateTeacher");
        }

        //Post dont send mail
        //[HttpPost]
        public ActionResult CreateUserAndSendMail(string UserName, string Email)
        {
            string CreateRandomPassword = GenerateRandomPassword(6);
            var EmailInData = db.tbl_User.SingleOrDefault(x => x.Email == Email);
            if (EmailInData != null)
            {
                ViewBag.Status = "Email đã tồn tại. Xin hãy nhập Email khác!";
                //return View("CreateTeacher", "User");
                return View("CreateTeacher");
            }
            else
            {
                tbl_User User = new tbl_User();
                if (ModelState.IsValid)
                {
                    User.Clock = false;
                    User.CreateDate = DateTime.Now;
                    User.DescriptionUser = null;
                    User.Email = Email;
                    User.LoginName = Email;
                    User.NumberPhone = null;
                    User.PasswordUser = HashPassword.Hash(CreateRandomPassword);
                    User.UserImage = null;
                    User.UserName = UserName;
                    User.Position = 3;
                    User.SendMail = true;
                    User.Notificationed = true;
                    try
                    {
                        //send mail to teacher

                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Admin/MailSendToTeacher.html"));

                        content= content.Replace("{{CustomName}}", User.UserName);
                        content = content.Replace("{{LoginName}}", Email);
                        content = content.Replace("{{Password}}", CreateRandomPassword);
                        var toMail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                        new MailHelper().SendMail(toMail, "Tài khoản vào EClass", content);
                    }
                    catch (Exception)
                    {
                        ViewBag.Status = "Không gửi được mail nên không tạo được tài khoản";
                        return View("CreateTeacher");
                    }


                    db.tbl_User.Add(User);
                    db.SaveChanges();
                    return RedirectToAction("ListTeacher", "Admin");
                }

            }

            ViewBag.Status = "Không tạo được tài khoản";
            return View("CreateTeacher");
        }


        //Clock and Unclock User
        public ActionResult ClockAndUnClock(int id)
        {
            var User = db.tbl_User.SingleOrDefault(x => x.ID == id);
            if(User.Clock==true)
            {
               
                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                User.Clock = false;
                db.SaveChanges();
                return RedirectToAction("ListTeacher", "Admin");
            }
            else
            {
               
                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                User.Clock = true;
                db.SaveChanges();
                return RedirectToAction("ListTeacher", "Admin");
            }
        }



        //Remove User

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var User = db.tbl_User.SingleOrDefault(x => x.ID == id);
            if (User == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(User);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var User = db.tbl_User.SingleOrDefault(x => x.ID == id);
            if(User==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tbl_User.Remove(User);
            db.SaveChanges();

            return RedirectToAction("ListTeacher", "Admin");
        }

        [HttpGet]
        public ActionResult CreateStudent()
        {
            return View();
        }
        //Student
        //[ActionName("CreateStudent")]
        [HttpPost]
        public ActionResult CreateStudent(tbl_User student)
        {
            var mailInData = db.tbl_User.SingleOrDefault(x => x.Email ==student.Email);
            if (mailInData != null)
            {
                ViewBag.ConfirmMail = "Email này đã đăng ký !";
                return View();
            }
            //if (password != confirmpassword)
            //{
            //    ViewBag.ConfirmPassword = "Mật khẩu xác nhận không đúng !";
            //    return View();
            //}
            tbl_User Student = new tbl_User();
            Student.Clock = false;
            Student.CreateDate = DateTime.Now;
            Student.DescriptionUser = null;
            Student.Email = student.Email;
            Student.LoginName = student.Email;
            Student.NumberPhone = student.NumberPhone;
            Student.PasswordUser = HashPassword.Hash(student.PasswordUser);
            Student.Position = 4;
            Student.SendMail = true;
            Student.UserImage = "no-image.jpg";
            Student.UserName = student.UserName;

            db.tbl_User.Add(Student);
            db.SaveChanges();
            ViewBag.Status = "Tạo tài khoản thành công";
            return View();


        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult ListStudent(int? page)
        {
            int pageSize = 5;

            int pageNumber = (page ?? 1);
            return View(db.tbl_User.Where(x => x.Position == 4).ToList().ToPagedList(pageNumber,pageSize));
        }

        public ActionResult DetailsStudent(int id)
        {
            var Student = db.tbl_User.Find(id);
            if(Student==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Student);
        }
        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            var Student = db.tbl_User.Find(id);
            if (Student == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Student);
        }

        [HttpPost,ActionName("DeleteStudent")]
        public ActionResult ConfirmDeleteStudent(int id)
        {
            var Student = db.tbl_User.Find(id);
            db.tbl_User.Remove(Student);
            db.SaveChanges();
            return RedirectToAction("ListStudent", "User");
        }


    }
}