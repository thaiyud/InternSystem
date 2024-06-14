﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Models
{
    public class GetAllCauHoiResponse
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
    }
}
