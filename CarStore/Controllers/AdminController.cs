using CarStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ICarRepository repository;

        public AdminController(ICarRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index() => View(repository.Cars);

        [HttpGet]
        public ViewResult Add() => View();

        [HttpPost]
        public IActionResult Add(Car car)
        {
            if (ModelState.IsValid)
            {
                this.repository.Save(car);
                TempData["message"] = $"{car.Name} has been saved.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int carId) => View(repository.Cars.FirstOrDefault(car => car.CarId == carId));

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                this.repository.Save(car);
                TempData["message"] = $"{car.Name} has been saved.";
                return RedirectToAction("Index");
            }

            return View(car);
        }

        [HttpPost]
        public IActionResult Delete(int carId)
        {
            if (ModelState.IsValid)
            {
                this.repository.Delete(carId);
                TempData["message"] = $"Car number {carId} has been deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}
