namespace InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Models
{
    public class GetCongNgheByTenResponse
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int IdViTri { get; set; }
        public string UrlImage { get; set; }

        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public string Errors { get; set; }
    }
}
