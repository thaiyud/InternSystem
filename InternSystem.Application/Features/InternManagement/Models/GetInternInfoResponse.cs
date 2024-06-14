using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.InternManagement.Models
{
    public class GetInternInfoResponse<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Results { get; set; }

        public GetInternInfoResponse(IEnumerable<T> results, int totalCount)
        {
            Results = results;
            TotalCount = totalCount;
        }
    }
}
