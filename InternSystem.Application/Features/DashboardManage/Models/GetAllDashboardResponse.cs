using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DashboardManage.Models
{
    public class GetAllDashboardResponse
    {
        public int ReceivedCV { get; set; }
        public int Interviewed { get; set; }
        public int Passed { get; set; }
        public int Interning { get; set; }
        public int Interned { get; set; }
        public string Errors { get; set; }
    }
}
