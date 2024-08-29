using NUnit.Framework;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services;
using Moq;

namespace RestoranoSistema.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using Moq;
    using RestoranoSistema.Services.Interfaces;

    [TestFixture]
    public class ReceiptServiceTests
    {
        private ReceiptsService _receiptService;

        [SetUp]
        public void Setup()
        {
            _receiptService = new ReceiptsService(Mock.Of<IReceiptRepository>());
        }

        [Test]
        public void GenerateClientReceipt_WithValidOrder_ReturnsExpectedReceipt()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                Table = new Table { Id = 1, Seats = 4 },
                OrderTime = new DateTime(2024, 8, 18, 12, 0, 0),
                Dishes = new List<Dish>
                {
                    new Dish { Name = "Burger", Price = 10.5m },
                    new Dish { Name = "Pizza", Price = 12.0m }
                },
                Beverages = new List<Beverage>
                {
                    new Beverage { Name = "Coke", Price = 2.5m },
                    new Beverage { Name = "Water", Price = 1.5m }
                }
            };

            // Act
            var result = _receiptService.GenerateClientReceipt(order);

            // Assert
            Assert.That(result.Count, Is.EqualTo(16));
            Assert.That(result[3], Is.EqualTo($"Uþsakymo Nr.: {orderId}"));
        }

        [Test]
        public void GenerateClientReceipt_EmptyOrder_ReturnsBasicReceipt()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                Table = new Table { Id = 2, Seats = 2 },
                OrderTime = new DateTime(2024, 8, 18, 13, 0, 0),
                Dishes = null,
                Beverages = null
            };

            // Act
            var result = _receiptService.GenerateClientReceipt(order);

            // Assert
            Assert.That(result.Count, Is.EqualTo(12)); 
            Assert.That(result[3], Is.EqualTo($"Uþsakymo Nr.: {orderId}"));
        }
    }

}
