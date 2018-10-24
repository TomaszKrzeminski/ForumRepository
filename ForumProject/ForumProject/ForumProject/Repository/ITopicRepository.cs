using ForumProject.Concrete;
using ForumProject.Entities;
using ForumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ForumProject.Repository
{
   






    public interface ITopicRepository
    {
        IEnumerable<Topic> Topics { get; }
        void Add(Topic entity);
        void Remove(Topic entity);
        Topic Get(int id);
        IEnumerable<Topic> GetAll();
        IEnumerable<Topic> Find(Expression<Func<Topic, bool>> predicate);
        void Add_To_Topics_And_User(Topic topic, string UserId);
        void Add_Comment(Comment comment, string UserId);
        
    }


    public interface IMainCategoryByCitiesRepository
    {

        IEnumerable<MainCategoryByCities> MainCategoryByCities { get; }
        void Add(MainCategoryByCities entity);
        void Remove(MainCategoryByCities entity);
        MainCategoryByCities Get(int id);
        IEnumerable<MainCategoryByCities> GetAll();
        IEnumerable<MainCategoryByCities> Find(Expression<Func<MainCategoryByCities, bool>> predicate);


    }


    public interface IIntermediateCategoryRepository
    {

        
        IEnumerable<IntermediateCategory> IntermediateCategories { get; }
        void Add(IntermediateCategory entity);
        void Remove(IntermediateCategory entity);
        IntermediateCategory Get(int id);
        IEnumerable<IntermediateCategory> GetAll();
        IEnumerable<IntermediateCategory> Find(Expression<Func<IntermediateCategory, bool>> predicate);


    }



















}