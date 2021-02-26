using Git.Data;
using Git.Data.Models;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitService : ICommitService
    {
        private readonly ApplicationDbContext db;

        public CommitService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void AddCommit(string descirption, string repoId, string userId)
        {
            db.Commits
                .Add(new Commit()
                {
                    Description = descirption,
                    RepositoryId = repoId,
                    CreatedOn = DateTime.Now,
                    CreatorId = userId
                });
            db.SaveChanges();
        }

        public void DeleteCommit(string id)
        {
            var toDelete = db.Commits.Find(id);
            db.Commits.Remove(toDelete);
            db.SaveChanges();
        }

        public ICollection<CommitAllViewModel> GetCommitsByUserId(string userId)
        {
            return db
                .Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitAllViewModel()
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Description = x.Description,
                    Repository = x.Repository.Name
                })
                .ToArray();
        }
    }
}
