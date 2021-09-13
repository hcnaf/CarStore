using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarStore.Infrastructure;
using CarStore.Models;
using System.Text.Json;
using CarStore.Models.ViewModels;
using Newtonsoft.Json;

namespace CarStore.Controllers
{
    public class CartController : Controller
    {
        private ICarRepository repository;
        private Cart cart;

        public CartController(ICarRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        public ViewResult Index(string url) => View(new CartViewModel() { Cart = cart, Url = url });

        public RedirectToActionResult AddToCart(int carId, string url)
        {
            var car = repository.Cars.FirstOrDefault(car => car.CarId == carId);
            if (car != null)
            {
                this.cart.AddItem(car, 1);
            }

            return RedirectToAction("Index", new { url });
        }

        public RedirectToActionResult RemoveFromCart(int carId, string url)
        {
            var car = repository.Cars.FirstOrDefault(car => car.CarId == carId);
            if (car != null)
            {
                this.cart.RemoveLine(car);
            }

            return RedirectToAction("Index", new { url });
        }
    }
}
