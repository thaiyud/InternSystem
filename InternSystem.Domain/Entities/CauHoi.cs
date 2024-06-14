using InternSystem.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternSystem.Domain.Entities
{
    [Table("CauHoi")]
    public class CauHoi : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public ICollection<CauHoiCongNghe> CauHoiCongNghes { get; set; }
    }
}
