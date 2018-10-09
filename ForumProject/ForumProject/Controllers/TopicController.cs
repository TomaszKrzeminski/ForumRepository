using ForumProject.Entities;
using ForumProject.Repository;
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


        public ViewResult Add_New_Topic(int id)
        {
            MainCategoryByCities maincategory = repository.Get_MainCategoryByCities_To_Add(id);
            ViewBag.InterId = maincategory.MainCategoryByCitiesId;

            return View(new Topic());
        }


        [HttpPost]
        public RedirectToRouteResult Add_New_Topic(Topic topic)
        {
            repository.Add_New_Topic_To_Database(topic);

            return RedirectToAction("Show_Topics",new {controller="Topic",action="Show_Topics",id=topic.IntermediateCategoryId });
        }



    }
}