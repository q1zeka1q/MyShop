using Microsoft.EntityFrameworkCore;
using Moq;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.SpaceshipTest
{
    public class SpaceshipTest
    {
        private SpaceshipsServices CreateService(out ShopTARgv24Context context)
        {
            var options = new DbContextOptionsBuilder<ShopTARgv24Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ShopTARgv24Context(options);

            var fileServiceMock = new Mock<IFileServices>();

            return new SpaceshipsServices(context, fileServiceMock.Object);
        }
        private SpaceshipDto MockSpaceshipData()
        {
            return new SpaceshipDto
            {
                Name = "Falcon",
                TypeName = "Cargo",
                BuiltDate = DateTime.Now,
                Crew = 5,
                EnginePower = 1000,
                Passengers = 10,
                InnerVolume = 200,
                CreatedAt = DateTime.Now
            };
        }

        // 1️ Test – kontrollib, et Create tagastab mitte-null objekti
        [Fact]
        public async Task Should_CreateSpaceship_WhenReturnResult()
        {
            // Arrange
            var service = CreateService(out var context);
            SpaceshipDto dto = MockSpaceshipData();

            // Act
            var result = await service.Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        // 2️ Test – kontrollib, et loodud kosmoselaeval on Id olemas
        [Fact]
        public async Task Should_CreateSpaceship_WithNotNullId()
        {
            // Arrange
            var service = CreateService(out var context);
            SpaceshipDto dto = MockSpaceshipData();

            // Act
            var result = await service.Create(dto);

            // Assert
            Assert.NotNull(result.Id);
        }

        // 3️ Test – kontrollib, et DetailAsync tagastab null kui Id ei leita
        [Fact]
        public async Task Should_ReturnNull_WhenSpaceshipNotFound()
        {
            // Arrange
            var service = CreateService(out var context);
            Guid wrongId = Guid.NewGuid();

            // Act
            var result = await service.DetailAsync(wrongId);

            // Assert
            Assert.Null(result);
        }

        // 4️ Test – kontrollib, et Delete eemaldab kosmoselaeva
        [Fact]
        public async Task Should_DeleteSpaceship_WhenDeleteById()
        {
            // Arrange
            var service = CreateService(out var context);
            SpaceshipDto dto = MockSpaceshipData();

            var created = await service.Create(dto);

            // Act
            var deleted = await service.Delete((Guid)created.Id);

            // Assert
            Assert.Equal(created.Id, deleted.Id);
            Assert.Equal(0, context.Spaceships.Count());
        }

        // 5️ Test – kontrollib, et Update muudab nime
        [Fact]
        public async Task Should_UpdateSpaceshipName_WhenUpdateData()
        {
            // Arrange
            var service = CreateService(out var context);
            SpaceshipDto dto = MockSpaceshipData();
            var created = await service.Create(dto);

            SpaceshipDto updateDto = new()
            {
                Id = created.Id,
                Name = "Updated Falcon",
                TypeName = created.TypeName,
                BuiltDate = created.BuiltDate,
                Crew = created.Crew,
                EnginePower = created.EnginePower,
                Passengers = created.Passengers,
                InnerVolume = created.InnerVolume,
                CreatedAt = created.CreatedAt
            };

            context.ChangeTracker.Clear();

            // Act
            var updated = await service.Update(updateDto);

            // Assert
            Assert.Equal(created.Id, updated.Id);
            Assert.NotEqual(dto.Name, updated.Name);
        }


    }
}
