using Microsoft.AspNetCore.Http;

namespace ShopTARgv24.Core.Dto
{
    public class KindergardenDto
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergardenName { get; set; }
        public string? TeacherName { get; set; }

        //Tuleb teha muutuja Files ja see peab olema listis
        public List<IFormFile> Files { get; set; }

        public IEnumerable<FileToApiDto> FileToApiDtos { get; set; }
            = new List<FileToApiDto>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
