using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarStore.Models
{
    public static class CarsSeed
    {
        public static void EnsurePopulated (IApplicationBuilder applicationBuilder)
        {
            var context = (CarsDbContext)applicationBuilder.ApplicationServices.GetService(typeof(CarsDbContext));
            context.Database.Migrate();
            if (!context.Cars.Any())
            {
                context.Cars.AddRange(
                    new Car()
                    { 
                        Name = "Porsche Cayman",
                        Category = "Coupe",
                        Price = 71.9m,
                        Description = "Perfect is his second name."
                    },
                    new Car()
                    {
                        Name = "Audi TT",
                        Category = "Coupe",
                        Price = 32m,
                        Description = "Coupe for your every day life"
                    },
                    new Car()
                    {
                        Name = "Volkswagen Passat CC",
                        Category = "Sedan",
                        Price = 26m,
                        Description = "Stylish sedan, that will never get old."
                    },
                    new Car()
                    {
                        Name = "Tesla Model 3",
                        Category = "Sedan",
                        Price = 43m,
                        Description = "Electrocar with a long range battarey."
                    },
                    new Car()
                    {
                        Name = "Audi A6",
                        Category = "Sedan",
                        Price = 29m,
                        Description = "Stylish family sedan."
                    },
                    new Car()
                    {
                        Name = "Jeep Grand Cherokee WK2",
                        Category = "Crossover",
                        Price = 23m,
                        Description = "Excelent crossover."
                    },
                    new Car()
                    {
                        Name = "BMW X6",
                        Category = "Crossover",
                        Price = 59.2m,
                        Description = "Sport crossover."
                    },
                    new Car()
                    {
                        Name = "Jaguar XF X260",
                        Category = "Sedan",
                        Price = 31.750m,
                        Description = "Luxry english sedan."
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
