﻿using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Models
{
    public class GetAllThongBaoResponse
    {
        public IEnumerable<ThongBao>? ThongBaos { get; set; }
        public string? Errors { get; set; }
    }
}
