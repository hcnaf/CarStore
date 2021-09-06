using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Models
{
    public interface ICarRepository
    {
        IQueryable<Car> Cars { get; }
    }
}
