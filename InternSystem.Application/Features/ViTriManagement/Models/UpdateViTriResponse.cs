using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ViTriManagement.Models
{
    public class UpdateViTriResponse
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? LinkNhomZalo { get; set; }
        public int? DuAnId { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }


        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }

        public string? Errors { get; set; }
    }
}
