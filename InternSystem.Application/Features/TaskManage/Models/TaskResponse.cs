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
    public class TaskResponse
    {
        public int Id { get; set; }
        public string DuAn { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGiao { get; set; }
        public DateTime HanHoanThanh { get; set; }

        public bool HoanThanh { get; set; }

        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }


    }
}
