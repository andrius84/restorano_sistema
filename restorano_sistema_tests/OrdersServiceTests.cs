using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using RestoranoSistema.Models;
using RestoranoSistema.Services;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services.Interfaces;

namespace RestoranoSistema.Tests
{
    [TestFixture]
    public class OrdersServiceTests
    {
        private OrdersService _ordersService;
        private Mock<IOrdersRepository> _mockOrderRepository;
        private List<Order> _orders;


        [SetUp]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IOrdersRepository>();
            _ordersService = new OrdersService(_mockOrderRepository.Object);

            _orders = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                Dishes = new List<Dish>
                {
                    new Dish { Id = 1, Name = "Pizza", Price = 12.0m },
                    new Dish { Id = 2, Name = "Burger", Price = 10.5m }
                },
                Beverages = new List<Beverage>
                {
                    new Beverage { Id = 1, Name = "Coke", Price = 2.5m }
                }
            }
        };

            // Mock ReadOrdersFromJsonFile to return the orders list
            _mockOrderRepository.Setup(repo => repo.ReadOrdersFromJsonFile()).Returns(_orders);
        }

        [Test]
        public void CreateOrder_ShouldAddOrderToJsonFile()
        {
            // Arrange
            var table = new Table();
            table.Id = 1;

            // Act
            _ordersService.CreateOrder(table);

            // Assert
            _mockOrderRepository.Verify(x => x.AddOrderToJsonFile(It.IsAny<Order>()), Times.Once);
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

        [Test]
        public void DeleteDishFromOrder_ValidDish_DeletesDishAndUpdatesOrder()
        {
            // Arrange
            var orderId = _orders[0].Id;
            var dishToDelete = _orders[0].Dishes[0]; // Dish with Id = 1

            // Act
            _ordersService.DeleteDishFromOrder(orderId, dishToDelete);

            // Assert
            Assert.IsFalse(_orders[0].Dishes.Any(d => d.Id == dishToDelete.Id), "The dish should be deleted from the order.");
            _mockOrderRepository.Verify(repo => repo.UpdateOrderToJsonFile(_orders[0]), Times.Once, "UpdateOrderToJsonFile should be called once.");
        }

        [Test]
        public void DeleteDishFromOrder_DishNotInOrder_NoChangesMade()
        {
            // Arrange
            var orderId = _orders[0].Id;
            var dishNotInOrder = new Dish { Id = 3, Name = "Salad", Price = 5.0m }; // Dish with Id = 3 is not in the order

            // Act
            _ordersService.DeleteDishFromOrder(orderId, dishNotInOrder);

            // Assert
            Assert.AreEqual(2, _orders[0].Dishes.Count, "The number of dishes should remain the same.");
            _mockOrderRepository.Verify(repo => repo.UpdateOrderToJsonFile(_orders[0]), Times.Once, "UpdateOrderToJsonFile should still be called.");
        }

        [Test]
        public void DeleteDishFromOrder_OrderNotFound_ThrowsException()
        {
            // Arrange
            var invalidOrderId = Guid.NewGuid();
            var dish = new Dish { Id = 1, Name = "Pizza", Price = 12.0m };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _ordersService.DeleteDishFromOrder(invalidOrderId, dish));
            Assert.AreEqual("Order not found", ex.Message);
            _mockOrderRepository.Verify(repo => repo.UpdateOrderToJsonFile(It.IsAny<Order>()), Times.Never, "UpdateOrderToJsonFile should not be called if order is not found.");
        }

        [Test]
        public void DeleteBeverageFromOrder_ValidBeverage_DeletesBeverageAndUpdatesOrder()
        {
            // Arrange
            var orderId = _orders[0].Id;
            var beverageToDelete = _orders[0].Beverages[0]; // Beverage with Id = 1

            // Act
            _ordersService.DeleteBeverageFromOrder(orderId, beverageToDelete);

            // Assert
            Assert.IsFalse(_orders[0].Beverages.Any(b => b.Id == beverageToDelete.Id), "The beverage should be deleted from the order.");
            _mockOrderRepository.Verify(repo => repo.UpdateOrderToJsonFile(_orders[0]), Times.Once, "UpdateOrderToJsonFile should be called once.");
        }

        [Test]
        public void DeleteBeverageFromOrder_BeverageNotInOrder_NoChangesMade()
        {
            // Arrange
            var orderId = _orders[0].Id;
            var beverageNotInOrder = new Beverage { Id = 2, Name = "Juice", Price = 3.0m }; // Beverage with Id = 2 is not in the order

            // Act
            _ordersService.DeleteBeverageFromOrder(orderId, beverageNotInOrder);

            // Assert
            Assert.AreEqual(1, _orders[0].Beverages.Count, "The number of beverages should remain the same.");
            _mockOrderRepository.Verify(repo => repo.UpdateOrderToJsonFile(_orders[0]), Times.Once, "UpdateOrderToJsonFile should still be called.");
        }

        [Test]
        public void DeleteBeverageFromOrder_OrderNotFound_ThrowsException()
        {
            // Arrange
            var invalidOrderId = Guid.NewGuid();
            var beverage = new Beverage { Id = 1, Name = "Coke", Price = 2.5m };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _ordersService.DeleteBeverageFromOrder(invalidOrderId, beverage));
            Assert.AreEqual("Order not found", ex.Message);
            _mockOrderRepository.Verify(repo => repo.UpdateOrderToJsonFile(It.IsAny<Order>()), Times.Never, "UpdateOrderToJsonFile should not be called if order is not found.");
        }
    }
}

