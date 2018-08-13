using Krustan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Krustan.Services;

namespace Krustan.Services
{
    public class DogService : IDogService
    {
        private static List<Dog> dogs = new List<Dog>();

        public DogService()
        {
            if (dogs.Count == 0)
            {
                dogs.Add(new Dog
                {
                    Name = "Cosmo",
                    Weight = 1.2f,
                    Height = 3.2f,
                    Description = "Jugueton, amigable",
                    Age = 2
                });
                dogs.Add(new Dog
                {
                    Name = "Locria",
                    Weight = 4,
                    Height = 6,
                    Description = "fuerte, amigable",
                    Age = 3
                });
                dogs.Add(new Dog
                {
                    Name = "Karma",
                    Weight = 4.1f,
                    Height = 12.5f,
                    Description = "jugueton, amigable, fuerte, coqueto...",
                    Age = 4
                });
            }
        }

        public Task<IEnumerable<Dog>> GetAll() => Task.Run(() => dogs.AsEnumerable());

        public Task<IEnumerable<Dog>> GetDogsByAge(int age)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dog>> GetDogsByBreed(string breed)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dog>> GetDogsByName(string name_part)
        {
            return Task.Run(() => dogs.Where(d => d.Name.ToLower().Contains(name_part.ToLower())));
        }
    }
}
