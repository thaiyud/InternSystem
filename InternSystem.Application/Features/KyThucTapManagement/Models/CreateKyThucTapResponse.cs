using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Models
{
    public class CreateKyThucTapResponse
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int IdTruong { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }

        public DateTimeOffset CreatedTime { get; set; } 
        public DateTimeOffset LastUpdatedTime { get; set; }

        public string? Errors { get; set; }
    }
}
