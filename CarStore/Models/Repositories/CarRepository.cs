using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Models
{
    public class CarRepository : ICarRepository
    {
        private CarsDbContext context;

        public CarRepository(CarsDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Car> Cars => context.Cars;

        public void Save(Car car)
        {
           if (car.CarId == 0)
           {
                this.context.Cars.Add(car);
                this.context.SaveChanges();
                return;
           }

            var carToSave = this.context.Cars.FirstOrDefault(c => c.CarId == car.CarId);
            if (carToSave != null)
            {
                carToSave.Name = car.Name;
                carToSave.Category = car.Category;
                carToSave.Description = car.Description;
                carToSave.Price = car.Price;
                carToSave.ImageURL = car.ImageURL;
                this.context.SaveChanges();
            }
        }

        public void Delete(int carId)
        {
            var carToDelete = this.context.Cars.FirstOrDefault(c => c.CarId == carId);
            if (carToDelete != null)
            {
                this.context.Cars.Remove(carToDelete);
                this.context.SaveChanges();
            }
        }
    }
}
