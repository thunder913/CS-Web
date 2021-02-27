using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString(); ;
        }
        public string Id { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(20)]
        public string Username { get; set; }
    
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        
        public string Password { get; set; }

        public ICollection<Repository> Repositories { get; set; } = new HashSet<Repository>();

        public ICollection<Commit> Commits { get; set; } = new HashSet<Commit>();
    }
}
