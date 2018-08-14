using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Krustan.Models
{
    public class Dog
    {
        public int DogId { get; set; }
        [Required(ErrorMessage = "Please, provide a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, provide the weight.")]
        public float Weight { get; set; }
        [Required(ErrorMessage = "Please, provide the height.")]
        public float Height { get; set; }
        [Required(ErrorMessage = "Please, provide a little description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please, provide the Dog's age.")]
        public int Age { get; set; }
        [Required()]
        public string Sex { get; set; } //1-male  2-female
        [Required]
        public string DogPicture { get; set; }
        [Required]
        public string OwnerId { get; set; }
    }
}
