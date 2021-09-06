using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarStore.Infrastructure;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using CarStore.Models.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using CarStore.Controllers;
using CarStore.Models;

namespace CarStore.Tests
{
    class PagingTest
    {
        [Test]
        public void Paging_GeneratePageLinks()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(factory => factory.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            var helper = new PageLinkHelper(urlHelperFactory.Object)
            {
                PageModel = new Paging() { CurrentPage = 2, TotalItems = 23, ItemsPerPage = 10 },
                PageAction = "Test"
            };

            var context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();

            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            helper.Process(context, output);

            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>" + @"<a href=""Test/Page2"">2</a>" + @"<a href=""Test/Page3"">3</a>", output.Content.GetContent());
        }

        [Test]
        public void Paging_CarsAreInDifferentPages()
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

            var cars = result.Cars.ToArray();

            Assert.AreEqual(cars.Length, 2);
            Assert.AreEqual("Car6", cars[0].Name);
            Assert.AreEqual("Car7", cars[1].Name);
        }
    }
}
