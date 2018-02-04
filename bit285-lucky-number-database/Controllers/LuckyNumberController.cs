using bit285_lucky_number_database.Models;
using lucky_number_database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bit285_lucky_number_database.Controllers
{

    public class LuckyNumberController : Controller
    {
        private LuckyNumberDbContext dbc = new LuckyNumberDbContext();

        [HttpGet]
        public ActionResult Spin()
        {
            LuckyNumber myLuck = new LuckyNumber { Number = 7, Balance = 4 };

            if (Session["CurrentID"] != null)
                myLuck = dbc.LuckyNumbers.Find((int)Session["CurrentID"]);

            return View(myLuck);
        }

        [HttpPost]
        public ActionResult Spin(LuckyNumber lucky)
        {
            if(lucky.Balance>0)
            {
                lucky.Balance -= 1;
            }
            if (Session["CurrentID"] == null)
                return View(lucky);

            dbc.LuckyNumbers.Find((int)Session["CurrentID"]).Balance = lucky.Balance;
            dbc.SaveChanges();

            return View(lucky);
        }

       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LuckyNumber newLuck)
        {
            dbc.LuckyNumbers.Add(newLuck);
            dbc.SaveChanges();

            Session["CurrentID"] = newLuck.LuckyId;

            return RedirectToAction("Spin");
        }
    }
}