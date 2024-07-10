namespace InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Models
{
    public class GetAllCongNgheResponse
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int IdViTri { get; set; }
        public string UrlImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
