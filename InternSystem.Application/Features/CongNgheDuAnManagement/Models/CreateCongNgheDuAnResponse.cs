using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Models
{
    public class CreateCongNgheDuAnResponse
    {
        public int IdCongNghe { get; set; }
        public int IdDuAn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string Errors { get; set; }
    }
}
