using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RestoranoSistema.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly RestoranasDbContext _context;

        public OrdersRepository(RestoranasDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding the order to the database: {ex.Message}");
            }
        }

        public List<Order> GetOrders()
        {
            try
            {
                return _context.Orders.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading orders from the database: {ex.Message}");
                return new List<Order>();
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (existingOrder != null)
                {
                    existingOrder.Dishes = order.Dishes;
                    existingOrder.Beverages = order.Beverages;
                    _context.Orders.Update(existingOrder);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating the order in the database: {ex.Message}");
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                var orderToDelete = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (orderToDelete != null)
                {
                    _context.Orders.Remove(orderToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting the order from the database: {ex.Message}");
            }
        }
    }
}