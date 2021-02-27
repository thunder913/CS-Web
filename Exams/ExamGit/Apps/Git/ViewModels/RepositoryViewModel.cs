using System;

namespace Git.ViewModels
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string OwnerName { get; set; }
        public int CommitsCount { get; set; }
    }
}
