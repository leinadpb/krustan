using Krustan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Krustan.ViewModels
{
    public class UserProfileViewModel
    {
        
        public String UniqueId { get; set; }

        public String Email { get; set; }

        public String Nickname { get; set; }

        public String ProfileImage { get; set; }

        public String Name { get; set; }

        public IEnumerable<Dog> MyDogs { get; set; }

        public IEnumerable<Dog> SavedDogs { get; set; }
    }
}
