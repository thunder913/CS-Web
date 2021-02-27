using Git.Data;
using Git.Data.Models;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class RepositoryService : IRepositoryService
    {
        private ApplicationDbContext db;

        public RepositoryService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public void AddRepository(string name, bool isPublic, string ownerId)
        {
            db
                .Repositories
                .Add(new Repository()
                {
                    OwnerId = ownerId,
                    CreatedOn = DateTime.Now,
                    IsPublic = isPublic,
                    Name = name,
                });
            db.SaveChanges();
        }

        public RepoCommitViewModel GetRepoCommitById(string id)
        {
            return db.Repositories
                .Where(x => x.Id == id)
                .Select(x => new RepoCommitViewModel()
                {
                    Id = id,
                    Name = x.Name
                })
                .First();
        }

        public ICollection<RepositoryViewModel> GetRepositoryViewModels(string userId)
        {
            return 
                db
                .Repositories
                .Where(x => x.IsPublic || x.OwnerId == userId)
                .Select(x => new RepositoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerName = x.Owner.Username,
                    CreatedOn = x.CreatedOn,
                    CommitsCount = x.Commits.Count
                })
                .ToList();
        }
    }
}
