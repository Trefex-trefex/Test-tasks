using System.IO;
using test_EM;

class Program
{
    static void Main(string[] args)
    {
        
        // Получение параметров из аргументов командной строки
        string logFilePath = args[0];
        string ordersFilePath = args[1];
        string district = args[2];
        DateTime firstDeliveryDateTime = DateTime.Parse(args[3]);
        string outputFilePath = args[4];
        
        var logger = new Logger(logFilePath);

        // Загрузка заказов из файла
        List<Order> orders = LoadOrders(ordersFilePath, logger);

        // Фильтрация заказов
        var filteredOrders = FilterOrders(orders, district, firstDeliveryDateTime);

        // Запись отфильтрованных заказов в файл
        SaveFilteredOrders(filteredOrders, outputFilePath, logger);

        logger.Log("Программа завершена.");
    }
    private static List<Order> FilterOrders(List<Order> orders, string district, DateTime firstDeliveryDateTime)
    {
        return orders
            .Where(o => o.District == district && o.DeliveryTime > firstDeliveryDateTime && o.DeliveryTime <= firstDeliveryDateTime.AddMinutes(30))
            .OrderBy(o => o.DeliveryTime)
            .ToList();
    }

    private static List<Order> LoadOrders(string path, Logger logger)
    {
        var orders = new List<Order>();
        try
        {
            foreach (var line in File.ReadLines(path))
            {
                var parts = line.Split(',');
                var order = new Order(parts[0], double.Parse(parts[1].Replace('.', ',')), parts[2], DateTime.Parse(parts[3]));
                if (Validator.IsValidOrder(order))
                {
                    orders.Add(order);
                }
                else
                {
                    logger.Log($"Неверные данные в строке: {line}");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Log($"Ошибка при загрузке заказов: {ex.Message}");
        }
        return orders;
    }

    private static void SaveFilteredOrders(List<Order> orders, string path, Logger logger)
    {
        try
        {
            {
                foreach (var order in orders)
                {
                    File.AppendAllText(path, $"{order.OrderId},{order.Weight},{order.District},{order.DeliveryTime}\n");
                    //writer.WriteLine($"{order.OrderId},{order.Weight},{order.District},{order.DeliveryTime}");
                }
            }
            logger.Log("Фильтрованные заказы успешно сохранены.");
        }
        catch (Exception ex)
        {
            logger.Log($"Ошибка при сохранении фильтрованных заказов: {ex.Message}");
        }
    }
}
