using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Models
{
    public class Card
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(15)]
        public string Name { get; set; }
    
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public int Attack { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public int Health { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<UserCard> UserCards { get; set; } = new HashSet<UserCard>();




    }
}
