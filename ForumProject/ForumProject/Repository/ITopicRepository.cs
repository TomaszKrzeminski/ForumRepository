using ForumProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumProject.Repository
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> Topics { get; }



    }


    public interface IMainCategoryByCitiesRepository
    {

        IEnumerable<MainCategoryByCities> MainCategoryByCities { get; }



    }


    public interface IIntermediateCategoryRepository
    {

        IEnumerable<IntermediateCategory> IntermediateCategory { get; }

    }








}