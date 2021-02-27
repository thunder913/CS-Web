using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
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

        public ICollection<UserCard> UserCards { get; set; } = new HashSet<UserCard>();
    
    }
}
