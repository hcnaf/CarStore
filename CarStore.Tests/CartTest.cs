using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarStore.Models;
using NUnit.Framework;

namespace CarStore.Tests
{
    class CartTest
    {
        [Test]
        public static void Cart_AddItem()
        {
            var carList = CarControllerTest.testCars;

            var cart = new Cart();
            for(int i = 0; i < carList.Length; ++i)
            {
                cart.AddItem(carList[i], i);
            }

            var linesArray = cart.CartLines.ToArray();

            Assert.AreEqual(6, linesArray.Length);
            Assert.AreEqual(carList[1], linesArray[0].Car);
            Assert.AreEqual(2, linesArray[1].Count);
        }

        [Test]
        public static void Cart_RemoveLine()
        {
            var carList = CarControllerTest.testCars;

            var cart = new Cart();
            for(int i = 0; i < carList.Length; ++i)
            {
                cart.AddItem(carList[i], i);
            }

            cart.RemoveLine(carList[4]);
            Assert.AreEqual(5, cart.CartLines.ToArray().Length);
        }

        [Test]
        public static void Cart_Total()
        {
            var carList = CarControllerTest.testCars;

            var cart = new Cart();
            for (int i = 0; i < carList.Length; ++i)
            {
                cart.AddItem(carList[i], i);
            }

            Assert.AreEqual(1120m, cart.Total);
        }
    }
}
