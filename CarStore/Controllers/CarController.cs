using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Models;
using CarStore.Models.ViewModels;

namespace CarStore.Controllers
{
    public class CarController : Controller
    {
        /// <summary>
        /// Car repository
        /// </summary>
        private ICarRepository repository;

        /// <summary>
        /// Maximum items on the page. Default = 5;
        /// </summary>
        public int PageSize = 5;

        /// <summary>
        /// Car controller ctor.
        /// </summary>
        /// <param name="repository"></param>
        public CarController(ICarRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public ViewResult List(string category, int page = 1) => View(new CarListViewModel()
        {
            Cars = repository.Cars.Where(car => category == null || car.Category == category).OrderBy(car => car.CarId).Skip((page - 1) * PageSize).Take(PageSize),
            Paging = new Paging() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = category is null ? repository.Cars.Count() : repository.Cars.Where(car => car.Category == category).Count() },
            CurrentCategory = category
        });
    }
}
