using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CarStore.Models;

namespace CarStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICarRepository carRepository;

        public NavigationMenuViewComponent(ICarRepository repository)
        {
            this.carRepository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(this.carRepository.Cars.Select(car => car.Category).Distinct().OrderBy(category => category));
        }
    }
}
