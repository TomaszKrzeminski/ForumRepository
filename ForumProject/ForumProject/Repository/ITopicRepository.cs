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
        IEnumerable<Topic> Get_Topics_ByIntermediateCategory(int id);
        MainCategoryByCities Get_MainCategoryByCities_To_Add(int id);
        bool Add_New_Topic_To_Database(Topic topic);

    }


    public interface IMainCategoryByCitiesRepository
    {

        IEnumerable<MainCategoryByCities> MainCategoryByCities { get; }



    }


    public interface IIntermediateCategoryRepository
    {

        IEnumerable<IntermediateCategory> IntermediateCategory { get; }


        IEnumerable<IntermediateCategory> GetIntermediateCategory_ById(int id);


    }








}