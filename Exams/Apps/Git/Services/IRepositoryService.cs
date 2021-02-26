using Git.ViewModels;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoryService
    {
        public ICollection<RepositoryViewModel> GetRepositoryViewModels(string userId);

        public void AddRepository(string name, bool isPublic, string ownerId);

        public RepoCommitViewModel GetRepoCommitById(string id);
    }
}
