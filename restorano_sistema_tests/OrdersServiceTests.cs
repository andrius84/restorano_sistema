using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using RestoranoSistema.Models;
using RestoranoSistema.Services;
using RestoranoSistema.Repositories.Interfaces;

namespace RestoranoSistema.Tests
{
    [TestFixture]
    public class OrdersServiceTests
    {
        private OrdersService _ordersService;
        private Mock<IOrdersRepository> _mockOrderRepository;

        [SetUp]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IOrdersRepository>();
            _ordersService = new OrdersService(_mockOrderRepository.Object);
        }

        [Test]
        public void CreateOrder_ShouldAddOrderToJsonFile()
        {
            // Arrange
            var table = new Table();

            // Act
            _ordersService.CreateOrder(table);

            // Assert
            _mockOrderRepository.Verify(x => x.AddOrderToJsonFile(It.IsAny<Order>()), Times.Once);
        }

        [Test]
        public void CalculateOrderTotalPrice_ShouldReturnTotalPrice()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                Dishes = new List<Dish>
                {
                    new Dish { Price = 10 },
                    new Dish { Price = 15 }
                },
                Beverages = new List<Beverage>
                {
                    new Beverage { Price = 5 },
                    new Beverage { Price = 8 }
                }
            };
            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            var totalPrice = _ordersService.CalculateOrderTotalPrice(orderId);

            // Assert
            Assert.AreEqual(38, totalPrice);
        }

        [Test]
        public void UpdateOrder_ShouldUpdateOrderInJsonFile()
        {
            // Arrange
            var order = new Order();

            // Act
            _ordersService.UpdateOrder(order);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrderToJsonFile(order), Times.Once);
        }

        [Test]
        public void DeleteOrder_ShouldDeleteOrderFromJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            _ordersService.DeleteOrder(orderId);

            // Assert
            _mockOrderRepository.Verify(x => x.DeleteOrderFromJsonFile(order), Times.Once);
        }

        [Test]
        public void AddDishToOrder_ShouldAddDishToOrderAndUpdateJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var dish = new Dish();

            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            _ordersService.AddDishToOrder(orderId, dish);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrderToJsonFile(order), Times.Once);
            Assert.Contains(dish, order.Dishes);
        }

        [Test]
        public void AddBeverageToOrder_ShouldAddBeverageToOrderAndUpdateJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var beverage = new Beverage();

            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            _ordersService.AddBeverageToOrder(orderId, beverage);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrderToJsonFile(order), Times.Once);
            Assert.Contains(beverage, order.Beverages);
        }

        [Test]
        public void DeleteDishFromOrder_ShouldRemoveDishFromOrderAndUpdateJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var dish = new Dish { Id = 1, Category = "Appetizers", Name = "Bruschetta", Price = 5.99m };
            order.Dishes?.Add(dish);

            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            _ordersService.DeleteDishFromOrder(orderId, dish);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrderToJsonFile(order), Times.Once);
            Assert.IsFalse(order.Dishes?.Contains(dish));
        }

        [Test]
        public void DeleteBeverageFromOrder_ShouldRemoveBeverageFromOrderAndUpdateJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var beverage = new Beverage { Id = 1, Category = "Beverages", Name = "Coffee", Price = 2.99m };
            order.Beverages?.Add(beverage);

            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            _ordersService.DeleteBeverageFromOrder(orderId, beverage);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrderToJsonFile(order), Times.Once);
            Assert.IsFalse(order.Beverages?.Contains(beverage));
        }

        [Test]
        public void GetOrders_ShouldReturnListOfOrders()
        {
            // Arrange
            var orders = new List<Order> { new Order(), new Order() };
            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(orders);

            // Act
            var result = _ordersService.GetOrders();

            // Assert
            Assert.AreEqual(orders, result);
        }

        [Test]
        public void GetOrderByTableId_ShouldReturnOrderWithMatchingTableId()
        {
            // Arrange
            var tableId = 1;
            var order = new Order { Table = new Table { Id = tableId } };
            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            var result = _ordersService.GetOrderByTableId(tableId);

            // Assert
            Assert.AreEqual(order, result);
        }

        [Test]
        public void GetOrderById_ShouldReturnOrderWithMatchingOrderId()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            var result = _ordersService.GetOrderById(orderId);

            // Assert
            Assert.AreEqual(order, result);
        }

        [Test]
        public void AddTotalPriceToOrder_ShouldUpdateOrderInJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _mockOrderRepository.Setup(x => x.ReadOrdersFromJsonFile()).Returns(new List<Order> { order });

            // Act
            _ordersService.AddTotalPriceToOrder(orderId);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrderToJsonFile(order), Times.Once);
        }
    }
}
