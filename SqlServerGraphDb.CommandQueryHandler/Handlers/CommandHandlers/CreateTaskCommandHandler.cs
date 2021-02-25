using MediatR;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SqlServerGraphDb.CommandQueryHandler.Handlers.CommandHandlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskRequestModel, CreateTaskResponseModel>
    {
        public Task<CreateTaskResponseModel> Handle(CreateTaskRequestModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
