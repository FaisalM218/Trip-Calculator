using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL.Models;

namespace TripCalculator
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalVariables.mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<DAL.Models.User, Models.User>();
                cfg.CreateMap<Models.User, DAL.Models.User>();
                cfg.CreateMap<DAL.Models.Trip, Models.Trip>();
                cfg.CreateMap<Models.Trip, DAL.Models.Trip> ();
                cfg.CreateMap<DAL.Models.Expense, Models.Expense>();
                cfg.CreateMap<Models.Expense, DAL.Models.Expense>();
                cfg.CreateMap<DAL.Models.Booking, Models.Booking>();
                cfg.CreateMap<Models.Booking, DAL.Models.Booking>();
            });
        }
    }
}
