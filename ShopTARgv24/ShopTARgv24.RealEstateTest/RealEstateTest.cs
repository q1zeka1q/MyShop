using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            RealEstateDto dto = new();

            dto.Area= 120.5;
            dto.Location= "123 Main St, Springfield";
            dto.RoomNumber= 3;
            dto.BuildingType= "Apartment";
            dto.CreateAt= DateTime.Now;
            dto.ModifiedAt= DateTime.Now;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);

        }
    }
}
