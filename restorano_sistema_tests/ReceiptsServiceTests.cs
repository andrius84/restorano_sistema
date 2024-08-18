using NUnit.Framework;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services;

namespace RestoranoSistema.Tests.Services
{
    [TestFixture]
    public class ReceiptsServiceTests
    {
        private ReceiptsService _receiptsService;

        [SetUp]
        public void Setup()
        {
            // Create a mock receipt repository
            var receiptRepository = new Mock<IReceiptRepository>().Object;

            // Create an instance of the ReceiptsService with the mock repository
            _receiptsService = new ReceiptsService(receiptRepository);
        }

        [Test]
        public void GenerateClientReceipt_ShouldReturnCorrectLines()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                Table = new Table { Id = 1, Seats = 4 },
                OrderTime = new DateTime(2022, 1, 1, 12, 0, 0),
                Dishes = new List<Dish>
                {
                    new Dish { Name = "Pizza", Price = 10.99 },
                    new Dish { Name = "Salad", Price = 5.99 }
                },
                Beverages = new List<Beverage>
                {
                    new Beverage { Name = "Coke", Price = 2.99 },
                    new Beverage { Name = "Water", Price = 1.99 }
                },
                TotalPrice = 21.96
            };

            var expectedLines = new List<string>
            {
                "---------------------------------------------------",
                "                       Kvitas                         ",
                "---------------------------------------------------",
                "Uþsakymo Nr.: 1",
                "Staliukas: 1 (Seats: 4)",
                "Uþsakymo laikas: 12:00 PM",
                "---------------------------------------------------",
                "Patiekalai ir gërimai:",
                "- Pizza                                  $10.99",
                "- Salad                                  $5.99",
                "- Coke                                   $2.99",
                "- Water                                  $1.99",
                "---------------------------------------------------",
                "Visa kaina:                              $21.96",
                "---------------------------------------------------",
                "Aèiû, kad renkatës mûsø restoranà!"
            };

            // Act
            var result = _receiptsService.GenerateClientReceipt(order);

            // Assert
            Assert.AreEqual(expectedLines, result);
        }

        [Test]
        public void GenerateRestaurantReceipt_ShouldReturnCorrectLines()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                Table = new Table { Id = 1, Seats = 4 },
                OrderTime = new DateTime(2022, 1, 1, 12, 0, 0),
                Dishes = new List<Dish>
                {
                    new Dish { Name = "Pizza", Price = 10.99 },
                    new Dish { Name = "Salad", Price = 5.99 }
                },
                Beverages = new List<Beverage>
                {
                    new Beverage { Name = "Coke", Price = 2.99 },
                    new Beverage { Name = "Water", Price = 1.99 }
                },
                TotalPrice = 21.96
            };

            var expectedLines = new List<string>
            {
                "---------------------------------------------------",
                "                   Restorano kvitas                   ",
                "---------------------------------------------------",
                "Uþsakymo numeris: 1",
                "Staliukas: 1 (Seats: 4)",
                "Uþsakymo laikas: 12:00 PM",
                "---------------------------------------------------",
                "Patiekalai ir gërimai:",
                "- Pizza                                  $10.99",
                "- Salad                                  $5.99",
                "- Coke                                   $2.99",
                "- Water                                  $1.99",
                "---------------------------------------------------",
                "Galutinë kaina:                          $21.96",
                "---------------------------------------------------"
            };

            // Act
            var result = _receiptsService.GenerateRestaurantReceipt(order);

            // Assert
            Assert.AreEqual(expectedLines, result);
        }
    }
}
