using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Models
{
    public class TaskReportResponse
    {      
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public string MoTa { get; set; }
        public string NoiDungBaoCao { get; set; }
        public DateTime NgayBaoCao { get; set; }
        public string TrangThai { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }

    }
}
