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
        public int MainCategoryByCitiesId { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<IntermediateCategory> IntermediateCategory { get; set; }
    }



    public class IntermediateCategory
    {
        public int IntermediateCategoryId { get; set; }

        public string NameOfMainCategory { get; set; }

        //public ICollection<MainCategoryByCities> MainCategoryByCities { get; set; }

        public int MainCategoryByCitiesId { get; set; }
        public MainCategoryByCities MainCategoryByCities { get; set; }

        public virtual ICollection<Topic> Topic { get; set; }

    }





    //Wątek
    public class Topic
    {
        public int TopicId { get; set; }

        public string TopicName { get; set; }

        [DataType(DataType.Date)]
        public DateTime TopicTime { get; set; }

        public int IntermediateCategoryId { get; set; }
        public IntermediateCategory IntermediateCategory { get; set; }


        public virtual ICollection<Comment> Comment { get; set; }


        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }


    //Temat
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
