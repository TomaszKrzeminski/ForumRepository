using ForumProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumProject.Controllers
{
    public class HomeController : Controller
    {

        private IMainCategoryByCitiesRepository repository;


        public HomeController(IMainCategoryByCitiesRepository repository)
        {
            this.repository = repository;
        }



        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {

            return View(repository.MainCategoryByCities);

        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}