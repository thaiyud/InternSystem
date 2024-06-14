using InternSystem.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Domain.Entities
{
    [Table("GroupZaloTask")]
    public class NhomZaloTask : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual Tasks Tasks { get; set; }

        [Required]
        public int NhomZaloId { get; set; }
        [ForeignKey("NhomZaloId")]
        public virtual NhomZalo nhomZalos { get; set; }
        public string? TrangThai { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
