using MediatR;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.QueryResponseModels;

namespace SqlServerGraphDb.CommandQueryHandler.RequestModels.QueryRequestModels
{
    public class TaskDetailsRequestModel : IRequest<TaskDetailsResponseModel>
    {
        public int TaskId { get; set; }
    }
}
