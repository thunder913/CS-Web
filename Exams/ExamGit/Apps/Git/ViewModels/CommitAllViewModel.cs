using System;

namespace Git.ViewModels
{
    public class CommitAllViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Repository { get; set; }
    }
}
