using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace stdDemoLogin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult stdLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult stdLogin(studentLogin stdLogin)
        {
            using (var context=new StudentDBEntities())
            {
                bool isValidStudent = context.studentLogin.Any(x => x.rollNumber == stdLogin.rollNumber && x.password == stdLogin.password);
                if (isValidStudent)
                {
                    FormsAuthentication.SetAuthCookie(stdLogin.rollNumber.ToString(),false);
                    return RedirectToAction("Index", "studentDetails");
                }
                else
                {
                    ModelState.AddModelError("", "invalid credentials");
                }
            }
            return View();
        }

        public ActionResult signUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult signUp(studentLogin student)
        {
            using (var context = new StudentDBEntities())
            {
                context.studentLogin.Add(student);
                context.SaveChanges();
            }
            return RedirectToAction("stdLogin");
        }


        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("stdLogin");
        }
    }
}