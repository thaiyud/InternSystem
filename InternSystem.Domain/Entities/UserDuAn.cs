using InternSystem.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternSystem.Domain.Entities
{
    [Table("UserDuAn")]
    public class UserDuAn : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AspNetUser User { get; set; }

        [Required]
        public int DuAnId { get; set; }
        [ForeignKey("DuAnId")]
        public virtual DuAn DuAn { get; set; }

        public int IdViTri { get; set; }
        [ForeignKey("IdViTri")]
        public virtual ViTri ViTri { get; set; }

        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
