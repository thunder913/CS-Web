
using Git.ViewModels;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitService
    {
        public void AddCommit(string descirption, string repoId, string userId);

        public ICollection<CommitAllViewModel> GetCommitsByUserId(string userId);

        public void DeleteCommit(string id);
    }
}
