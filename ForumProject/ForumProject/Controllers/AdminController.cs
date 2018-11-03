using ForumProject.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumProject.Entities;

namespace ForumProject.Controllers
{
    public class AdminController : Controller
    {

        ITopicRepository repository;
        IIntermediateCategoryRepository repositoryInter;
        IMainCategoryByCitiesRepository repositoryMain;
        private Func<string> GetUserId;
        public AdminController(ITopicRepository repository, IIntermediateCategoryRepository repositoryInter, IMainCategoryByCitiesRepository repositoryMain)
        {
            this.repository = repository;
            this.repositoryInter = repositoryInter;
            this.repositoryMain = repositoryMain;
            GetUserId = () => User.Identity.GetUserId();
        }




        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ChangeCities()
        {
            //List<MainCategoryByCities> list = repositoryMain.GetAll().ToList();

            //if(list!=null)
            //{
            //    return View(list);
            //}
            //else
            //{
            // return View(list=new List<MainCategoryByCities>());
            //}


            try
            {

                return View(repositoryMain.GetAll().ToList());

            }

            catch (Exception ex)
            {

                return View(new List<MainCategoryByCities>());

            }







        }



        public ActionResult ChangeCategories(int id)
        {
            MainCategoryByCities main;
            main = repositoryMain.Get(id);


            if (main.CityName == "None")
            {
                
                ViewBag.ErrorMessage = "There isn't a City with that Id ";
                return View("Error");
            }
            else
            {
                List<IntermediateCategory> list = main.IntermediateCategory.ToList();
                return View(list);
            }

            
        }



        public ActionResult AddCategory(int id)
        {

            IntermediateCategory category = new IntermediateCategory();
            category.MainCategoryByCitiesId = id;


            return View(category);
        }

        [HttpPost]
        public ActionResult AddCategory(IntermediateCategory result)
        {


            bool DoesItExist = repositoryMain.AddIntermediateCategory(result);

            if (DoesItExist == true)
            {
                ViewBag.ErrorMessage = "Category Exists";
                return View("Error");
            }
            else
            {
                //return View("SuccesAddingCategory");
                return RedirectToAction("ChangeCategories", new { controller = "Admin", action = "ChangeCategories", id = result.MainCategoryByCitiesId });
            }


        }




        public ActionResult DeleteCategory(int id)
        {

            IntermediateCategory category = repositoryInter.Get(id);
            //ViewBag.CategoryName = category.NameOfMainCategory;

            bool result=repositoryInter.Remove(category);

            if(result==false)
            {
                ViewBag.ErrorMessage = "You have to keep at least one Category";
                return View("Error");
            }


            //return View("DeleteCategory");
            return RedirectToAction("ChangeCategories", new { controller = "Admin", action = "ChangeCategories", id = category.MainCategoryByCitiesId });
        }


        public ActionResult EditCategory(int id)
        {

            IntermediateCategory result = repositoryInter.Get(id);
            if (result.NameOfMainCategory == "Error")
            {
                ViewBag.ErrorMessage = "Category with that Id does not exists";
                return View("Error");
            }


            return View(result);



        }

        [HttpPost]
        public ActionResult EditCategory(IntermediateCategory category)
        {
            IntermediateCategory result = repositoryInter.Get(category.IntermediateCategoryId);

            //if(result.NameOfMainCategory=="Error")
            //{
            //    ViewBag.ErrorMessage = "Category with that Id does not exists";
            //    return View("Error");
            //}

            bool check = repositoryInter.ChangeIntermediateCategory(category);

            if (check)
            {
                return RedirectToAction("ChangeCategories", new { controller = "Admin", action = "ChangeCategories", id = result.MainCategoryByCitiesId });
            }
            else
            {
                ViewBag.ErrorMessage = "Category Exists";
                return View("Error");
            }



        }


        public ActionResult DeleteTopic(int id)
        {
            Topic topic = repository.Get(id);
            repository.Remove(topic);


            return RedirectToAction("Show_Topics", new { controller = "Topic", action = "Show_Topics", id = topic.IntermediateCategoryId });

        }

        public ActionResult RemoveComment(int id)
        {
            int IdTopic = repository.GetTopicIdCommentId(id);
            bool result = repository.DeleteCommentFromTopic(id);


            if (result)
            {
                return RedirectToAction("Go_To_Topic", new { controller = "Topic", action = "Go_To_Topic", id = IdTopic });
            }
            else
            {
                ViewBag.ErrorMessage = "Problem with delecting Comment";
                return View("Error");
            }

        }




    }
}