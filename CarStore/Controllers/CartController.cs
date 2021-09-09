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
        public CartController(ICarRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(string url)
        {
            var cart = GetCart();
            var viewModel = new CartViewModel() { Cart = cart, Url = url };
            return View(viewModel);
        }

        public RedirectToActionResult AddToCart(int carId, string url)
        {
            var car = repository.Cars.FirstOrDefault(car => car.CarId == carId);
            if (car != null)
            {
                Cart cart = GetCart();
                cart.AddItem(car, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { url });
        }

        public RedirectToActionResult RemoveFromCart(int carId, string url)
        {
            var car = repository.Cars.FirstOrDefault(car => car.CarId == carId);
            if (car != null)
            {
                var cart = GetCart();
                cart.RemoveLine(car);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { url });
        }

        private Cart GetCart() 
        {
            var value = HttpContext.Session.GetString("Cart");
            return value is null ? new Cart() : JsonConvert.DeserializeObject<Cart>(value);
        }

        private void SaveCart(Cart cart) => HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }
}
