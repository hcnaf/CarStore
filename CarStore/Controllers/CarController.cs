using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarStore.Models;

namespace CarStore.Controllers
{
    public class CarController : Controller
    {
        /// <summary>
        /// Car repository
        /// </summary>
        private ICarRepository repository;

        /// <summary>
        /// Car controller ctor.
        /// </summary>
        /// <param name="repository"></param>
        public CarController(ICarRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException();
        }

        public ViewResult List() => View(repository.Products);
    }
}
