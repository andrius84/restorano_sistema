using RestoranoSistema.Models;
using RestoranoSistema.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptService(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;

        }
        public List<string> GenerateClientReceipt(Order order)
        {
            var lines = new List<string>
        {
            "----------------------------------------",
            "               Client Receipt           ",
            "----------------------------------------",
            $"Order ID: {order.Id}",
            $"Table: {order.Table.Id} (Seats: {order.Table.Seats})",
            $"Order Time: {order.OrderTime.ToShortTimeString()}",
            "----------------------------------------",
            "Items:"
        };
            if (order.Dishes != null)
            {
                foreach (var dish in order.Dishes)
                {
                    lines.Add($"- {dish.Name}           ${dish.Price:F2}");
                }
            }
            if (order.Beverages != null)
            {
                foreach (var beverage in order.Beverages)
                {
                    lines.Add($"- {beverage.Name}           ${beverage.Price:F2}");
                }
            }
            lines.AddRange(new[]
            {
            "----------------------------------------",
            $"Total Price:       ${order.TotalPrice:F2}",
            "----------------------------------------",
            "Thank you for dining with us!"
        });
            return lines;
        }
        public List<string> GenerateRestaurantReceipt(Order order)
        {
            var lines = new List<string>
        {
            "----------------------------------------",
            "            Restaurant Receipt          ",
            "----------------------------------------",
            $"Order ID: {order.Id}",
            $"Table: {order.Table.Id} (Seats: {order.Table.Seats})",
            $"Order Time: {order.OrderTime.ToShortTimeString()}",
            "----------------------------------------",
            "Items:"
        };
            if (order.Dishes != null)
            {
                foreach (var dish in order.Dishes)
                {
                    lines.Add($"- {dish.Name}           ${dish.Price:F2}");
                }
            }
            if (order.Beverages != null)
            {
                foreach (var beverage in order.Beverages)
                {
                    lines.Add($"- {beverage.Name}           ${beverage.Price:F2}");
                }
            }
            lines.AddRange(new[]
            {
            "----------------------------------------",
            $"Total Price:       ${order.TotalPrice:F2}",
            "----------------------------------------"
        });
            _receiptRepository.SaveRestaurantReceiptToFile(lines);
            return lines;
        }
    }
}
