using ForumProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumProject.Models
{
    public class AddingCommentViewModel
    {
      public  AddingCommentViewModel()
        {
            comment = new Comment();
        }

        public Topic topic { get; set; }
        public Comment comment { get; set; }



    }
}