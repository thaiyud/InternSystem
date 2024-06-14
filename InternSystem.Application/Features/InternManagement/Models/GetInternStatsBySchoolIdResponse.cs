using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.InternManagement.Models
{
    public class GetInternStatsBySchoolIdResponse
    {
        public int KyThucTapId { get; set; }
        public string KyThucTapName { get; set; }
        public int StudentCount { get; set; }

        
    }
}
