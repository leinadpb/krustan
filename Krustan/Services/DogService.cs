using Krustan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Krustan.Services;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using Amazon.Runtime;

namespace Krustan.Services
{
    public class DogService : IDogService
    {
        private static List<Dog> dogs = new List<Dog>();
        private IAmazonS3 client;
        private IConfiguration Configuration;

        public DogService(IConfiguration cfg)
        {
            Configuration = cfg;

            var credentials = new BasicAWSCredentials(Configuration["s3:AccessKey"].Trim(), Configuration["s3:SecretAccessKey"].Trim());
            client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
            
            if (dogs.Count == 0)
            {
                dogs.Add(new Dog
                {
                    Name = "Cosmo",
                    Weight = 1.2f,
                    Height = 3.2f,
                    Description = "Jugueton, amigable",
                    Age = 2,
                    DogPicture = @"https://images.dog.ceo/breeds/deerhound-scottish/n02092002_1339.jpg",
                    Sex = "male",
                    OwnerId = "123456789"
                });
                dogs.Add(new Dog
                {
                    Name = "Locria",
                    Weight = 4,
                    Height = 6,
                    Description = "fuerte, amigable",
                    Age = 3,
                    DogPicture = @"https://images.dog.ceo/breeds/appenzeller/n02107908_3190.jpg",
                    Sex = "male",
                    OwnerId = "123456789"
                });
                dogs.Add(new Dog
                {
                    Name = "Karma",
                    Weight = 4.1f,
                    Height = 12.5f,
                    Description = "juguetona, amigable, fuerte, coqueta...",
                    Age = 4,
                    DogPicture = @"https://images.dog.ceo/breeds/samoyed/n02111889_5463.jpg",
                    Sex = "female",
                    OwnerId = "123456789"
                });
            }
        }

        public Task<Dog> AddDog(Dog dog) => Task.Run(() =>
        {
            dogs.Add(dog);
            return dog;
        });

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

        public Task<string> UploadPictureToS3Bucket(string pathToFile, string extension)
        {
            return Task.Run(async () => {

                string pathToS3 = "";

                FileStream stream = new FileStream(pathToFile, FileMode.Open);

                String timeStamp = GetTimestamp(DateTime.Now);

                Random random = new Random();
                var fileNameInS3 = "dog-image-" + random.Next(0, 98000).ToString() + "_" + timeStamp + extension;

                PutObjectRequest request = new PutObjectRequest();
                request.InputStream = stream;
                request.BucketName = Configuration["s3:BucketName"];
                request.CannedACL = S3CannedACL.PublicRead;
                request.Key = "dogs/" + fileNameInS3;
                request.AutoCloseStream = true;

                var response = await client.PutObjectAsync(request);

                string baseS3 = @"https://s3.amazonaws.com/krustan/dogs/";

                return pathToS3 = baseS3 + fileNameInS3;
            });
        }
        public string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public Task<IEnumerable<Dog>> GetDogsByUser(string userid)
        {
            return Task.Run( () => dogs.Where(d => d.OwnerId.Equals(userid)) );
        }
    }
}
