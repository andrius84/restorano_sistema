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
            string projectDirectory = "../../../../restorano_sistema/Data/food.csv";
            string projectDirectory2 = "../../../../restorano_sistema/Data/drinks.csv";
            _itemsRepository = new ItemsRepository(projectDirectory, projectDirectory2);
            _itemsService = new ItemsService(_itemsRepository);
        }
    }
}
