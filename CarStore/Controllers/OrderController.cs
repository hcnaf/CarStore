using Microsoft.AspNetCore.Mvc;
using CarStore.Models;
using System;
using System.Linq;

namespace CarStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repository, Cart cart)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (this.cart.CartLines.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty :(");
            }

            if (ModelState.IsValid)
            {
                order.Lines = this.cart.CartLines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Success));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Success()
        {
            this.cart.Clear();
            return View();
        }
    }
}
