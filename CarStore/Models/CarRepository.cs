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

        public IQueryable<Car> Products => context.Cars;
    }
}
