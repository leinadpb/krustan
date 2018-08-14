using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Krustan.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        [Required]
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public string Nickname { get; set; }
        public string ProfileImage { get; set; }

        public List<Dog> Dogs { get; set; }

    }
}
