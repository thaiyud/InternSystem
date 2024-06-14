using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Interview.Models
{
    public class GetDetailCommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int IdNguoiDuocComment { get; set; }
        public string IdNguoiComment { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string? Errors {  get; set; }
    }
}
