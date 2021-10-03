using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace CarStore.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session.GetString("Cart") is null ? new SessionCart() : JsonConvert.DeserializeObject<SessionCart>(session.GetString("Cart"));
            cart.Session = session;
            return cart;
        }

        private ISession Session { get; set; }

        public override void AddItem(Car car, int count)
        {
            base.AddItem(car, count);
            Session.SetString("Cart", JsonConvert.SerializeObject(this));
        }

        public override void RemoveLine(Car car)
        {
            base.RemoveLine(car);
            Session.SetString("Cart", JsonConvert.SerializeObject(this));
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
