using MediatR;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;

namespace SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels
{
    public class CreateTaskRequestModel : IRequest<CreateTaskResponseModel>
    {
        public string TaskName { get; set; }
    }
}
