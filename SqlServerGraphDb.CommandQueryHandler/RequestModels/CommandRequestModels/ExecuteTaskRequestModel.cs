using MediatR;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;

namespace SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels
{
    public class ExecuteTaskRequestModel : IRequest<ExecuteTaskResponseModel>
    {
        public int TaskId { get; set; }
    }
}
