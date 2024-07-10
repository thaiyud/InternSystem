namespace InternSystem.Application.Features.InternManagement.TruongHocManagement.Models
{
    public class GetPagedTruongHocsResponse
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int SoTuanThucTap { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
    }
}
