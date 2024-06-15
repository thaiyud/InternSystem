using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Models
{
        public class ExampleResponse
        {
            public int NhomZaloId { get; set; }
            public string Leader { get; set; }
            public string Mentor { get; set; }
            public TaskDetails Task { get; set; }
        }

        public class TaskDetails
        {
            public int DuanId { get; set; }
            public string Leader { get; set; }
        }
    }

