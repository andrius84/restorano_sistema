using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using RestoranoSistema.Entities;
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
            _mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(_orders);
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
            _mockOrderRepository.Verify(x => x.AddOrder(It.IsAny<Order>()), Times.Once);
        }

        [Test]
        public void UpdateOrder_ShouldUpdateOrderInJsonFile()
        {
            // Arrange
            var order = new Order { Id = Guid.NewGuid() };

            // Act
            _ordersService.UpdateOrder(order);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrder(order), Times.Once);
        }

        [Test]
        public void AddDishToOrder_ShouldAddDishToOrderAndUpdateJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var dish = new Dish();

            _mockOrderRepository.Setup(x => x.GetOrders()).Returns(new List<Order> { order });

            // Act
            _ordersService.AddDishToOrder(orderId, dish);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrder(order), Times.Once);
            Assert.Contains(dish, order.Dishes);
        }

        [Test]
        public void AddBeverageToOrder_ShouldAddBeverageToOrderAndUpdateJsonFile()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var beverage = new Beverage();

            _mockOrderRepository.Setup(x => x.GetOrders()).Returns(new List<Order> { order });

            // Act
            _ordersService.AddBeverageToOrder(orderId, beverage);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrder(order), Times.Once);
            Assert.Contains(beverage, order.Beverages);
        }

        [Test]
        public void GetOrders_ShouldReturnListOfOrders()
        {
            // Arrange
            var orders = new List<Order> { new Order(), new Order() };
            _mockOrderRepository.Setup(x => x.GetOrders()).Returns(orders);

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
            _mockOrderRepository.Setup(x => x.GetOrders()).Returns(new List<Order> { order });

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
            _mockOrderRepository.Setup(x => x.GetOrders()).Returns(new List<Order> { order });

            // Act
            var result = _ordersService.GetOrderById(orderId);

            // Assert
            Assert.AreEqual(order, result);
        }
    }
}

