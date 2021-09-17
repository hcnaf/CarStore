using Microsoft.AspNetCore.Mvc;
using CarStore.Models;

namespace CarStore.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}
