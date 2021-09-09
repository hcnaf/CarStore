using NUnit.Framework;
using Moq;
using CarStore.Models;
using System.Linq;
using CarStore.Controllers;
using System.Collections.Generic;
using CarStore.Models.ViewModels;

namespace CarStore.Tests
{
    public class CarControllerTest
    {
        internal static Car[] testCars = new Car[]
        {
            new Car()
            {
                CarId = 1,
                Name = "Car1",
                Category = "Category1",
                Price = 10m,
            },
            new Car()
            {
                CarId = 2,
                Name = "Car2",
                Category = "Category2",
                Price = 20m,
            },
            new Car()
            {
                CarId = 3,
                Name = "Car3",
                Category = "Category2",
                Price = 30m,
            },
            new Car()
            {
                CarId = 4,
                Name = "Car4",
                Category = "Category2",
                Price = 40m,
            },
            new Car()
            {
                CarId = 5,
                Name = "Car5",
                Category = "Category3",
                Price = 50m,
            },
            new Car()
            {
                CarId = 6,
                Name = "Car6",
                Category = "Category3",
                Price = 60m,
            },
            new Car()
            {
                CarId = 7,
                Name = "Car7",
                Category = "Category1",
                Price = 70m,
            },
        };

        [Test]
        public void List_PaginatingIntoManyPages()
        {
            var mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(testCars.AsQueryable());

            var controller = new CarController(mock.Object);

            var result = controller.List(null, 2).ViewData.Model as CarListViewModel;

            var pageInfo = result.Paging;
            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(5, pageInfo.ItemsPerPage);
            Assert.AreEqual(7, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }

        [Test]
        public void List_Filtering()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(cars => cars.Cars).Returns(testCars.AsQueryable());

            var controller = new CarController(mock.Object);

            var result = (controller.List("Category3", 1).ViewData.Model as CarListViewModel).Cars.ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0].Name == "Car5" && result[0].Category == "Category3");
            Assert.IsTrue(result[1].Name == "Car6" && result[1].Category == "Category3");
        }
    }
}