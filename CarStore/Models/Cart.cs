using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Models
{
    public class Cart
    {
        private List<CartLine> lines = new List<CartLine>();
        public virtual void AddItem(Car car, int count)
        {
            if (count < 1)
            {
                return;
            }

            var line = this.lines.Where(line => line.Car.CarId == car.CarId).FirstOrDefault();
            if (line is null)
            {
                lines.Add(new CartLine
                {
                    Car = car,
                    Count = count
                });
            }
            else
            {
                line.Count += count;
            }
        }

        public virtual void RemoveLine(Car car) => lines.RemoveAll(line => line.Car == car);

        public virtual decimal Total => lines.Sum(line => line.Car.Price * line.Count);

        public virtual void Clear() => this.lines.Clear();

        public virtual IEnumerable<CartLine> CartLines => lines;
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Car Car { get; set; }
        public int Count { get; set; }
    }
}
