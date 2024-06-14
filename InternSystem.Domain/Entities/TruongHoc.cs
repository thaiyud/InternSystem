using InternSystem.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternSystem.Domain.Entities
{
    [Table("TruongHoc")]
    public class TruongHoc : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Ten { get; set; }
        public int SoTuanThucTap { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
