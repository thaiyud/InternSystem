﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Models
{
    public class UpdateCongNgheDuAnResponse
    {
        public int Id { get; set; }
        public int IdCongNghe { get; set; }
        public int IdDuAn { get; set; }
        public string LastUpdatedBy { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public string Errors { get; set; }
    }
}
