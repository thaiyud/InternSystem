﻿using InternSystem.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Domain.Entities
{
    [Table("Task")]
    public class Tasks : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DuAnId { get; set; }
        [ForeignKey("DuAnId")]
        public virtual DuAn DuAn { get; set; }

        [Required]
        public string MoTa { get; set; }
        [Required]
        public string NoiDung { get; set; }

        public DateTime NgayGiao { get; set; } 

        public DateTime HanHoanThanh { get; set; }

        public bool HoanThanh { get; set; }

        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }

        public virtual ICollection<ReportTask> ReportTasks { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<NhomZaloTask> NhomZaloTasks { get; set; }
    }
}
