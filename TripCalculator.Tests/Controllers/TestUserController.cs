using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Controllers;
using TripCalculator.Models;
using System.Web.Mvc;

namespace TripCalculator.Tests.Controllers
{
    /// <summary>
    /// Summary description for TestUserController
    /// </summary>
    [TestClass]
    public class TestUserController
    {
        [TestMethod]
        public void TestCreate()
        {
            //This test calls the Create method of the User Controller to ensure that we get a redirect action.
            UsersController controller = new UsersController(new TestCalculatorContext());
            User u = new User() { FirstName ="bob", LastName="john", UserId=1, UserName="BobJohn" };
            ActionResult res = controller.Create(u);
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeRes = (RedirectToRouteResult)res;
            Assert.AreEqual(routeRes.RouteValues["action"], "Index");
        }
    }
}
