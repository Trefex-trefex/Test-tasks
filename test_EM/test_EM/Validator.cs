using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_EM
{
    public class Validator
    {
        public static bool IsValidOrder(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderId) || order.Weight <= 0 || string.IsNullOrEmpty(order.District))
                return false;
            return true;
        }
    }
}
