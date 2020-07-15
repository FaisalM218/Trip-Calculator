using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Controllers;
using TripCalculator.Models;
using System.Web.Mvc;
using BLL;
using AutoMapper;

namespace TripCalculator.Tests.Controllers
{
    /// <summary>
    /// Summary description for TestUserController
    /// </summary>
    [TestClass]
    public class TestUserController
    {

        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            GlobalVariables.mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<DAL.Models.User, TripCalculator.Models.User>();
                cfg.CreateMap<TripCalculator.Models.User, DAL.Models.User>();
                cfg.CreateMap<DAL.Models.Trip, TripCalculator.Models.Trip>();
                cfg.CreateMap<TripCalculator.Models.Trip, DAL.Models.Trip>();
                cfg.CreateMap<DAL.Models.Expense, TripCalculator.Models.Expense>();
                cfg.CreateMap<TripCalculator.Models.Expense, DAL.Models.Expense>();
                cfg.CreateMap<DAL.Models.Booking, TripCalculator.Models.Booking>();
                cfg.CreateMap<TripCalculator.Models.Booking, DAL.Models.Booking>();
            });
        } 

        [TestMethod]
        public void TestCreate()
        {
            //This test calls the Create method of the User Controller to ensure that we get a redirect action.
            UsersController controller = new UsersController(new UserProcessor(new TestCalculatorContext()));
            User u = new User() { FirstName ="bob", LastName="john", UserId=1, UserName="BobJohn" };
            ActionResult res = controller.Create(u);
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeRes = (RedirectToRouteResult)res;
            Assert.AreEqual(routeRes.RouteValues["action"], "Index");
        }
    }
}
