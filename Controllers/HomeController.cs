using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookReview.Controllers
{/// <summary>
/// Контролер Домашньої сторінки
/// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Головна сторінка
        /// </summary>
        /// <returns></returns>
        [RequireHttps]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Сторінка про нас
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        /// <summary>
        /// Сторінка контакти
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}