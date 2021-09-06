using System.Collections.Generic;
using CarStore.Models;

namespace CarStore.Models.ViewModels
{
    public class CarListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public Paging Paging { get; set; }
    }
}
