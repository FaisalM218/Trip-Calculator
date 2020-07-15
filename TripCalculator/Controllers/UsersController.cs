using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripCalculator.Models;

namespace TripCalculator.Controllers
{
    public class UsersController : Controller
    {
        private UserProcessor userProcessor = new UserProcessor();
        private IMapper mapper = GlobalVariables.mapper.CreateMapper();

        public UsersController() { }
        public UsersController(UserProcessor processor)
        {
            userProcessor = processor;
        }

        // GET: Users
        public ActionResult Index()
        {
            List<User> users = userProcessor.getAllUsers().Select(u => mapper.Map<User>(u)).ToList();
            return View(users);
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
                userProcessor.addUser(mapper.Map<DAL.Models.User>(user));
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}
