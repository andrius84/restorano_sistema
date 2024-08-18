using NUnit.Framework;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services;

namespace RestoranoSistema.Tests
{
    [TestFixture]
    public class ItemsServiceTests
    {
        private IItemsRepository _itemsRepository;
        private ItemsService _itemsService;

        [SetUp]
        public void Setup()
        {
            string projectDirectory = "../../../../Data/food.csv";
            string projectDirectory2 = "../../../../Data/drinks.csv";
            _itemsRepository = new ItemsRepository(projectDirectory, projectDirectory2);
            _itemsService = new ItemsService(_itemsRepository);
        }

        [Test]
        public void Beverages_ReturnsBeverageListInDescendingOrder()
        {
            // Arrange
            var expectedBeverages = _itemsRepository.GetBeverageList().OrderByDescending(x => x.Name).ToList();

            // Act
            var actualBeverages = _itemsService.Beverages();

            // Assert
            CollectionAssert.AreEqual(expectedBeverages, actualBeverages);
        }

        [Test]
        public void Dishes_ReturnsFoodListInDescendingOrder()
        {
            // Arrange
            var expectedDishes = _itemsRepository.GetFoodList().OrderByDescending(x => x.Name).ToList();

            // Act
            var actualDishes = _itemsService.Dishes();

            // Assert
            CollectionAssert.AreEqual(expectedDishes, actualDishes);
        }
    }
}
