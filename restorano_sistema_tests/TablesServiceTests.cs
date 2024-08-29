using NUnit.Framework;
using Moq;
using RestoranoSistema.Entities;
using RestoranoSistema.Services;
using RestoranoSistema.Repositories.Interfaces;

namespace RestoranoSistema.Tests
{
    [TestFixture]
    public class TablesServiceTests
    {
        private Mock<ITableRepository> _tableRepositoryMock;
        private TablesService _tablesService;

        [SetUp]
        public void Setup()
        {
            _tableRepositoryMock = new Mock<ITableRepository>();
            _tablesService = new TablesService(_tableRepositoryMock.Object);
        }

        [Test]
        public void GetTable_ValidTableId_ReturnsTable()
        {
            // Arrange
            int tableId = 1;
            var table = new Table { Id = tableId };
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table> { table });

            // Act
            var result = _tablesService.GetTable(tableId);

            // Assert
            Assert.AreEqual(table, result);
        }

        [Test]
        public void GetTable_InvalidTableId_ReturnsNull()
        {
            // Arrange
            int tableId = 1;
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table>());

            // Act
            var result = _tablesService.GetTable(tableId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void MarkTableAsOccupied_ValidTableId_MarksTableAsOccupied()
        {
            // Arrange
            int tableId = 1;
            var table = new Table { Id = tableId };
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table> { table });

            // Act
            _tablesService.MarkTableAsOccupied(tableId);

            // Assert
            Assert.IsTrue(table.IsOccupied);
            _tableRepositoryMock.Verify(r => r.SaveTables(table), Times.Once);
        }

        [Test]
        public void MarkTableAsOccupied_InvalidTableId_DoesNotMarkTableAsOccupied()
        {
            // Arrange
            int tableId = 1;
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table>());

            // Act
            _tablesService.MarkTableAsOccupied(tableId);

            // Assert
            _tableRepositoryMock.Verify(r => r.SaveTables(It.IsAny<Table>()), Times.Never);
        }

        [Test]
        public void MarkTableAsFree_ValidTableId_MarksTableAsFree()
        {
            // Arrange
            int tableId = 1;
            var table = new Table { Id = tableId, IsOccupied = true };
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table> { table });

            // Act
            _tablesService.MarkTableAsFree(tableId);

            // Assert
            Assert.IsFalse(table.IsOccupied);
            _tableRepositoryMock.Verify(r => r.SaveTables(table), Times.Once);
        }

        [Test]
        public void MarkTableAsFree_InvalidTableId_DoesNotMarkTableAsFree()
        {
            // Arrange
            int tableId = 1;
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table>());

            // Act
            _tablesService.MarkTableAsFree(tableId);

            // Assert
            _tableRepositoryMock.Verify(r => r.SaveTables(It.IsAny<Table>()), Times.Never);
        }

        [Test]
        public void ChooseTable_ValidTableId_MarksTableAsOccupied()
        {
            // Arrange
            int tableId = 1;
            var table = new Table { Id = tableId };
            _tableRepositoryMock.Setup(r => r.LoadTables()).Returns(new List<Table> { table });

            // Act
            _tablesService.ChooseTable(tableId);

            // Assert
            Assert.IsTrue(table.IsOccupied);
            _tableRepositoryMock.Verify(r => r.SaveTables(table), Times.Once);
        }
    }
}   
