using InternSystem.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternSystem.Domain.Entities
{
    [Table("CongNghe")]
    public class CongNghe : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Ten { get; set; }

        public int? IdViTri { get; set; }
        [ForeignKey("IdViTri")]
        public virtual ViTri? ViTri { get; set; }

        public string? UrlImage { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public virtual ICollection<CongNgheDuAn> CongNgheDuAns { get; set; }
        public ICollection<CauHoiCongNghe> CauHoiCongNghes { get; set; }
    }
}
