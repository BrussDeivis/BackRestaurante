using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class EscritorioController : BaseController
    {
        public ActionResult Estadistica()
        {
            try
            {
                return View();

            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}