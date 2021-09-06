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
        [Test]
        public void List_PaginatingIntoManyPages()
        {
            var mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car()
                {
                    CarId = 1,
                    Name = "Car1",
                },
                new Car()
                {
                    CarId = 2,
                    Name = "Car2",
                },
                new Car()
                {
                    CarId = 3,
                    Name = "Car3",
                },
                new Car()
                {
                    CarId = 4,
                    Name = "Car4",
                },
                new Car()
                {
                    CarId = 5,
                    Name = "Car5",
                },
                new Car()
                {
                    CarId = 6,
                    Name = "Car6",
                },
                new Car()
                {
                    CarId = 7,
                    Name = "Car7",
                },
            }.AsQueryable());

            var controller = new CarController(mock.Object);

            var result = controller.List(2).ViewData.Model as CarListViewModel;

            var pageInfo = result.Paging;
            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(5, pageInfo.ItemsPerPage);
            Assert.AreEqual(7, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }
    }
}