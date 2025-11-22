using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.KindergardenTest;

namespace ShopTARgv24.KindergardenTest
{
    public class KindergardenTest : TestBase
    {

        [Fact]
        public async Task Should_CreateKindergarden_WhenDataIsValid()
        {
            // Test kontrollib, et uus lasteaed salvestatakse

            // Arrange
            KindergardenDto dto = MockKindergardenData();

            // Act
            var result = await Svc<IKindergardensServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.GroupName, result.GroupName);
        }


        [Fact]
        public async Task Should_GetKindergardenById_WhenExists()
        {
            // Test kontrollib, et detail päring tagastab loodud kirje

            // Arrange
            KindergardenDto dto = MockKindergardenData();
            var created = await Svc<IKindergardensServices>().Create(dto);

            // Act
            var result = await Svc<IKindergardensServices>()
                .DetailAsync((Guid)created.Id);

            // Assert
            Assert.Equal(created.Id, result.Id);
            Assert.Equal(created.GroupName, result.GroupName);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenIdNotFound()
        {
            // Test kontrollib, et vale ID tagastab null

            // Arrange
            Guid wrongId = Guid.NewGuid();

            // Act
            var result = await Svc<IKindergardensServices>().DetailAsync(wrongId);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task ShouldNot_Update_WhenIdNotFound()
        {
            // Test kontrollib, et uuendamine ei tööta vale ID-ga

            // Arrange
            var updateDto = MockUpdateKindergardenData();
            updateDto.Id = Guid.NewGuid();

            // Act
            var result = await Svc<IKindergardensServices>().Update(updateDto);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task Should_DeleteKindergarden_WhenExists()
        {
            // Test kontrollib, et kustutamine töötab

            // Arrange
            KindergardenDto dto = MockKindergardenData();
            var created = await Svc<IKindergardensServices>().Create(dto);

            // Act
            var deleted = await Svc<IKindergardensServices>()
                .Delete((Guid)created.Id);

            // Assert
            Assert.Equal(created.Id, deleted.Id);
        }


        [Fact]
        public async Task Should_AssignUniqueId_WhenCreatingMultiple()
        {
            // Test kontrollib, et igale kirjele tehakse erinev ID

            // Arrange
            KindergardenDto dto1 = MockKindergardenData();
            KindergardenDto dto2 = MockKindergardenData();

            // Act
            var k1 = await Svc<IKindergardensServices>().Create(dto1);
            var k2 = await Svc<IKindergardensServices>().Create(dto2);

            // Assert
            Assert.NotEqual(k1.Id, k2.Id);
        }
        private KindergardenDto MockKindergardenData()
        {
            return new KindergardenDto
            {
                GroupName = "Karud",
                ChildrenCount = 20,
                KindergardenName = "Lilleke",
                TeacherName = "Mari",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        private KindergardenDto MockUpdateKindergardenData()
        {
            return new KindergardenDto
            {
                GroupName = "Päikesed",
                ChildrenCount = 15,
                KindergardenName = "Sipsik",
                TeacherName = "Kati",
                UpdatedAt = DateTime.Now.AddMinutes(1)
            };
        }
    }
}
