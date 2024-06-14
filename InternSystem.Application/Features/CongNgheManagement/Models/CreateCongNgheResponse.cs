﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Models
{
    public class CreateCongNgheResponse
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int IdViTri { get; set; }
        public string UrlImage { get; set; }
        public string CreateBy { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }
        public string Errors { get; set; }
    }
}
