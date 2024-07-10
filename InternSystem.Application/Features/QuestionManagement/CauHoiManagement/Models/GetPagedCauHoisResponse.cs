﻿namespace InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Models
{
    public class GetPagedCauHoisResponse
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
    }
}
