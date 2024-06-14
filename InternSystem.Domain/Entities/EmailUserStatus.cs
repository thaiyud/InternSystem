using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InternSystem.Domain.Entities;
using InternSystem.Domain.Entities.BaseEntities;

namespace InternSystem.Domain.Entities
{
    [Table("EmailUserStatus")]
    public class EmailUserStatus : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string IdNguoiGui { get; set; }
        [ForeignKey("IdNguoiGui")]
        public virtual AspNetUser NguoiGui { get; set; }

        public int IdNguoiNhan { get; set; }
        [ForeignKey("IdNguoiNhan")]
        public virtual InternInfo NguoiNhan { get; set; }

        public string EmailLoai1 { get; set; }
        public string EmailLoai2 { get; set; }
        public string EmailLoai3 { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
