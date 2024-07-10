using InternSystem.Application.Features.TasksAndReports.TaskManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.TasksAndReports.TaskManagement.Queries
{
    public class GetTaskQuery : IRequest<IEnumerable<TaskResponse>>
    {
    }
}
