using ForumProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumProject.Models
{
    public class TopicViewModel
    {

      public TopicViewModel()
        {
            comment_List = new List<Comment>();
        }

      public  Topic topic { get; set; }
      public  string userName { get; set; }
      public  List<Comment> comment_List { get; set; }
      

    }
}