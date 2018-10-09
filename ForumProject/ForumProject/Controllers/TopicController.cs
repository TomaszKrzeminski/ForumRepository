using ForumProject.Entities;
using ForumProject.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumProject.Controllers
{
    public class TopicController : Controller
    {
        ITopicRepository repository;
        public TopicController(ITopicRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Show_Topics(int id)
        {

            ViewBag.IntermediateCategory_Id = id;

            List<Topic> TopicList = repository.Get_Topics_ByIntermediateCategory(id).ToList();


            return View(TopicList);
        }

        [Authorize]
        public ViewResult Add_New_Topic(int id)
        {
            //MainCategoryByCities maincategory = repository.Get_MainCategoryByCities_To_Add(id);
            //ViewBag.InterId = maincategory.MainCategoryByCitiesId;

            ViewBag.InterId = id;

            return View(new Topic());
        }


        [HttpPost]
        public RedirectToRouteResult Add_New_Topic(Topic topic)
        {

            string UserId= User.Identity.GetUserId();
            repository.Add_New_Topic_To_Database(topic,UserId);

            return RedirectToAction("Show_Topics",new {controller="Topic",action="Show_Topics",id=topic.IntermediateCategoryId });
        }

        public ActionResult Go_To_Topic(int id)
        {


            return View();
        }

    }
}