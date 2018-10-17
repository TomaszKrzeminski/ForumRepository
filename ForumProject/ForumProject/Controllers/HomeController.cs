using ForumProject.Entities;
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
        private IIntermediateCategoryRepository repositoryIntermediate;

        public HomeController(IMainCategoryByCitiesRepository repository, IIntermediateCategoryRepository repositoryIntermediate)
        {
            this.repository = repository;
            this.repositoryIntermediate = repositoryIntermediate;
        }



        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {

            return View(repository.MainCategoryByCities);

        }



        public ViewResult Show_IntermediateCategory_List(int id)
        {
            List<IntermediateCategory> list = new List<IntermediateCategory>();

            

            list = repository.Get(id).IntermediateCategory.ToList();

            return View(list);

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