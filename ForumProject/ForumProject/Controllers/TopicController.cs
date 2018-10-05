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

        public ViewResult List()
        {
            return View(repository.Topics);
        }
    }
}