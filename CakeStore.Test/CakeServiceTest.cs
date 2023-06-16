using AutoMapper;
using CakeStore.BL.Services;
using CakeStore.DL.Interfaces;
using CakeStore.Models.Models;
using CakeStore.Models.Request.Cake;
using Moq;

namespace CakeStore.Test
{
    public class CakeServiceTest
    {
        [Fact]
        public async Task AddCake_ValidFactoryId_ReturnsCake()
        {
            // Arrange
            var addCakeRequest = new AddCakeRequest
            {
                Name = "Gosho",
                FactoryId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
            };

            var factoryRepositoryMock = new Mock<IFactoryRepository>();
            factoryRepositoryMock.Setup(repo => repo.Exists(addCakeRequest.FactoryId))
                .ReturnsAsync(true);

            var cakeRepositoryMock = new Mock<ICakeRepository>();
            cakeRepositoryMock.Setup(repo => repo.Add(It.IsAny<Cake>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Cake>(addCakeRequest))
                .Returns(new Cake
                {
                    Id = Guid.NewGuid(),
                    Name = "Gosho",
                    FactoryId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
                });

            var cakeService = new CakeService(cakeRepositoryMock.Object, mapperMock.Object, factoryRepositoryMock.Object);

            // Act
            var result = await cakeService.Add(addCakeRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Cake>(result);
        }

        [Fact]
        public async Task AddCake_InvalidFactoryId_ReturnsNull()
        {
            // Arrange
            var addCakeRequest = new AddCakeRequest
            {
                Name = "Gosho",
                FactoryId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
            };

            var factoryRepositoryMock = new Mock<IFactoryRepository>();
            factoryRepositoryMock.Setup(repo => repo.Exists(addCakeRequest.FactoryId))
                .ReturnsAsync(false);

            var cakeRepositoryMock = new Mock<ICakeRepository>();
            var mapperMock = new Mock<IMapper>();

            var cakeService = new CakeService(cakeRepositoryMock.Object, mapperMock.Object, factoryRepositoryMock.Object);

            // Act
            var result = await cakeService.Add(addCakeRequest);

            // Assert
            Assert.Null(result);
        }
    }
}
