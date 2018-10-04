using ForumProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumProject.Entities
{
    public class MainCategoryByCities
    {
        public int MainCategoryID { get; set; }
        public string CityName { get; set; }

        public ICollection<IntermediateCategory> IntermediateCategory { get; set; }
    }



    public class IntermediateCategory
    {
        public int IntermediateCategoryID { get; set; }

        public string NameOfMainCategory { get; set; }

        public int MainCategoryID { get; set; }
        public MainCategoryByCities MainCategoryByCities { get; set; }

        public ICollection<Topic> Topic { get; set; }

    }






    public class Topic
    {
        public int TopicID { get; set; }

        public int TopicName { get; set; }

        [DataType(DataType.Date)]
        public DateTime TopicTime { get; set; }

        public int IntermediateCategoryID { get; set; }
        public IntermediateCategory IntermediateCategory { get; set; }


        public ICollection<Comment> Comment { get; set; }


        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }



    public class Comment
    {
        public int CommentID { get; set; }

        public string CommentContent { get; set; }

        [DataType(DataType.Date)]
        public DateTime CommentTime { get; set; }

        public int TopicID { get; set; }
        public Topic Topic { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }




}
