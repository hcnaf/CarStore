using NUnit.Framework;
using Moq;
using CarStore.Controllers;
using CarStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Tests
{
    class OrderControllerTest
    {
        [Test]
        public void Checkout_CartIsEmpty_ModelStateIsInvalid()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();

            var target = new OrderController(mock.Object, cart);
            var result = target.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [Test]
        public void Checkout_InvlidShippingDetails()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Car(), 2);

            var target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));

            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [Test]
        public void Checkout_CartAndShippingDetailsAreValid()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Car(), 2);

            var target = new OrderController(mock.Object, cart);

            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.AreEqual("Success", result.ActionName);
        }
    }
}
