using Moq;
using NUnit.Framework;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services;

namespace RestoranoSistema.Tests
{
    [TestFixture]
    public class ItemsServiceTests
    {
        private ItemsService _itemsService;
        private Mock<IItemsRepository> _mockItemsRepository;

        [SetUp]
        public void Setup()
        {
            _mockItemsRepository = new Mock<IItemsRepository>();
            _itemsService = new ItemsService(_mockItemsRepository.Object);
        }

        [Test]
        public void Beverages_ShouldReturnBeveragesOrderedByNameDescending()
        {
            var beverages = new List<Beverage>
        {
            new Beverage { Name = "Coke" },
            new Beverage { Name = "Pepsi" },
            new Beverage { Name = "Sprite" }
        };
            _mockItemsRepository.Setup(repo => repo.GetBeverageList()).Returns(beverages);

            var result = _itemsService.Beverages();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Name, Is.EqualTo("Sprite"));
            Assert.That(result[1].Name, Is.EqualTo("Pepsi"));
            Assert.That(result[2].Name, Is.EqualTo("Coke"));

            _mockItemsRepository.Verify(repo => repo.GetBeverageList(), Times.Once);
        }

        [Test]
        public void Dishes_ShouldReturnDishesOrderedByNameDescending()
        {
            var dishes = new List<Dish>
        {
            new Dish { Name = "Burger" },
            new Dish { Name = "Pizza" },
            new Dish { Name = "Pasta" }
        };

            _mockItemsRepository.Setup(repo => repo.GetFoodList()).Returns(dishes);

            var result = _itemsService.Dishes();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Name, Is.EqualTo("Pizza")); 
            Assert.That(result[1].Name, Is.EqualTo("Pasta"));
            Assert.That(result[2].Name, Is.EqualTo("Burger"));

            _mockItemsRepository.Verify(repo => repo.GetFoodList(), Times.Once);
        }

    }
}
