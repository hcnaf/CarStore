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
                        Price = 71999.9m,
                        Description = "Perfect is his second name."
                    },
                    new Car()
                    {
                        Name = "Audi TT",
                        Category = "Coupe",
                        Price = 32200m,
                        Description = "Coupe for your every day life"
                    },
                    new Car()
                    {
                        Name = "Volkswagen Passat CC",
                        Category = "Sedan",
                        Price = 26700m,
                        Description = "Stylish sedan, that will never get old."
                    },
                    new Car()
                    {
                        Name = "Tesla Model 3",
                        Category = "Sedan",
                        Price = 43000m,
                        Description = "Electrocar with a long range battarey."
                    },
                    new Car()
                    {
                        Name = "Audi A6",
                        Category = "Sedan",
                        Price = 29700m,
                        Description = "Stylish family sedan."
                    },
                    new Car()
                    {
                        Name = "Jeep Grand Cherokee WK2",
                        Category = "Crossover",
                        Price = 23000m,
                        Description = "Excelent crossover."
                    },
                    new Car()
                    {
                        Name = "BMW X6",
                        Category = "Crossover",
                        Price = 59200m,
                        Description = "Sport crossover."
                    },
                    new Car()
                    {
                        Name = "Jaguar XF X260",
                        Category = "Sedan",
                        Price = 31750m,
                        Description = "Luxry english sedan."
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
