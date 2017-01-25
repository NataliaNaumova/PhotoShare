using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace MvcPL.Controllers
{
    public class ErrorController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Error()
        {
            logger.Info("A user made a forbidden action");

            return View();
        }

        public ActionResult NotFound()
        {
            logger.Info("A user requested a page that does not exist.");

            return View();
        }
    }
}
