using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Models
{
    public class TestCarRepository : ICarRepository
    {
        public IQueryable<Car> Cars => new List<Car> { new Car() { Name = "Porsche Cayman", Category = "Coupe", Price = 71999.9m, Description = "Perfect is his second name."},
        new Car() { Name = "Audi TT", Category = "Coupe", Price = 32200m, Description = "Coupe for your every day life"},
        new Car() { Name = "Volkswagen Passat CC", Category = "Sedan", Price = 26700m, Description = "Stylish sedan, that will never get old."} }.AsQueryable();
    }
}
