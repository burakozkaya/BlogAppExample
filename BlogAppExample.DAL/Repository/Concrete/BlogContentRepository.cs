﻿using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogAppExample.DAL.Repository.Concrete;

public class BlogContentRepository : GenericRepository<BlogContent>, IBlogContentRepo
{
    public BlogContentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public override IEnumerable<BlogContent> GetAll()
    {
        return _dbSet
            .Include(x => x.Category)
            .Include(x => x.AppUser)
            .ToList();
    }

    public override IEnumerable<BlogContent> GetByPredicate(Expression<Func<BlogContent, bool>> predicate)
    {
        return _dbSet.Include(x => x.Category)
            .Where(predicate)
            .ToList();
    }

    public override BlogContent? GetById(int id)
    {
        return _dbSet.Include(x => x.Category).Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<BlogContent> GetMostReaded()
    {
        return _dbSet.OrderByDescending(x => x.NumberOfReads).Take(10);
    }

    public IEnumerable<BlogContent> GetUserBlog(string userId)
    {
        return GetAll().Where(x => x.AppUserId == userId).OrderByDescending(x => x.CreatedDate);
    }
    public void IncrementReadCount(int blogContentId)
    {
        var blogContent = _dbSet.Find(blogContentId);

        blogContent.NumberOfReads++;

    }

}