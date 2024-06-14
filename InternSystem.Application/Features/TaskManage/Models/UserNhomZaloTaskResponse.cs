using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Models
{
    public class UserNhomZaloTaskResponse
    {
        public int InterviewId { get; set; }
        public string KetQua { get; set; }
        public string GroupName { get; set; }
        public string GroupLink { get; set; }
        public string CreateBy { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }

    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
}

