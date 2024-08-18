using NUnit.Framework;
using Moq;
using RestoranoSistema.Models;
using RestoranoSistema.Services.Interfaces;
using RestoranoSistema.UI.Interfaces;
using RestoranoSistema.Services;
using RestoranoSistema.UI;

namespace RestoranoSistema.Tests
{
    [TestFixture]
    public class UserInterfaceTests
    {
        private Mock<ITablesService> _tableServiceMock;
        private Mock<IOrdersService> _orderServiceMock;
        private Mock<IReceiptsService> _receiptServiceMock;
        private Mock<IItemsService> _itemServiceMock;
        private IUserInterface _userInterface;

        [SetUp]
        public void Setup()
        {
            _tableServiceMock = new Mock<ITablesService>();
            _orderServiceMock = new Mock<IOrdersService>();
            _receiptServiceMock = new Mock<IReceiptsService>();
            _itemServiceMock = new Mock<IItemsService>();
            _userInterface = new UserInterface(_tableServiceMock.Object, _orderServiceMock.Object, _receiptServiceMock.Object, _itemServiceMock.Object);
        }

    }
}
