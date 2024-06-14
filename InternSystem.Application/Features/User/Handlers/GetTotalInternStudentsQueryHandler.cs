using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    namespace InternSystem.Application.Features.User.Handlers
    {
        public class GetTotalInternStudentsQueryHandler : IRequestHandler<GetTotalInternStudentsQuery, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetTotalInternStudentsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(GetTotalInternStudentsQuery request, CancellationToken cancellationToken)
            {
                var totalInternStudents = await _unitOfWork.InternInfoRepository.GetTotalInternStudentsByDateRangeAsync(request.StartDate, request.EndDate);
                return totalInternStudents;
            }
        }
    }
}
