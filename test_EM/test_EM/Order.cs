using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_EM
{
    public class Order
    {
        public string OrderId { get; set; }
        public double Weight { get; set; }
        public string District { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Order(string orderId, double weight, string district, DateTime deliveryTime)
        {
            OrderId = orderId;
            Weight = weight;
            District = district;
            DeliveryTime = deliveryTime;
        }
    }
}
