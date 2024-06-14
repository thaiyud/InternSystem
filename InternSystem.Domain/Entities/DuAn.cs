using InternSystem.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternSystem.Domain.Entities
{
    [Table("DuAn")]
    public class DuAn : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Ten { get; set; }

        public string? LeaderId { get; set; }
        [ForeignKey("LeaderId")]
        public virtual AspNetUser? Leader { get; set; }

        public DateTimeOffset ThoiGianBatDau { get; set; }
        public DateTimeOffset ThoiGianKetThuc { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }

        public virtual ICollection<UserDuAn> UserDuAns { get; set; }
        public virtual ICollection<CongNgheDuAn> CongNgheDuAns { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }

}
