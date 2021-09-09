using CarStore.Components;
using CarStore.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CarStore.Tests
{
    class NavigationMenuViewComponentTest
    {
        [Test]
        public void Invoke_SelectCategories_ReturnsOrderedCategories()
        {
            var mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(CarControllerTest.testCars.AsQueryable());
            var target = new NavigationMenuViewComponent(mock.Object);

            var result = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            Assert.AreEqual(new string[] { "Category1", "Category2", "Category3" }, result);
        }
    }
}
