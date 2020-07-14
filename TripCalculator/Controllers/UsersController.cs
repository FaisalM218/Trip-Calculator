using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TripCalculator.Data_Access_Layer;
using TripCalculator.Models;

namespace TripCalculator.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ICalculatorContext db = new CalculatorContext();

        public UsersController() { }

        public UsersController(ICalculatorContext context)
        {
            this.db = context;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //GET: Users/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserId,UserName,Password")] User user)
        {
            User res = db.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if(res == null)
            {
                ModelState.AddModelError("", "User name and password don't match");
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index");
            }
        }

        //GET: Users/SignUp
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Users/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult SignUp([Bind(Include = "UserId,UserName,Password,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //GET: Users/Logout
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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
