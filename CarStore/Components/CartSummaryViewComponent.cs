using CarStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cart) =>
            this.cart = cart ?? throw new ArgumentNullException(nameof(cart));

        public IViewComponentResult Invoke() => View(this.cart);
    }
}
