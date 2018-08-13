using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Krustan.Models;

namespace Krustan.Models
{
    public interface IDogService
    {
        Task<IEnumerable<Dog>> GetAll();
        Task<IEnumerable<Dog>> GetDogsByAge(int age);
        Task<IEnumerable<Dog>> GetDogsByName(string name_part);
        Task<IEnumerable<Dog>> GetDogsByBreed(string breed);
    }
}
