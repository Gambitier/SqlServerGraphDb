using MediatR;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using SqlServerGraphDb.Persistence.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace SqlServerGraphDb.CommandQueryHandler.Handlers.CommandHandlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskRequestModel, CreateTaskResponseModel>
    {
        private readonly ITaskPersistence _taskPersistence;

        public CreateTaskCommandHandler(ITaskPersistence taskPersistence)
        {
            _taskPersistence = taskPersistence;
        }

        public async Task<CreateTaskResponseModel> Handle(CreateTaskRequestModel request, CancellationToken cancellationToken)
        {
            var taskId = await _taskPersistence.CreateTaskByName(request.TaskName);
            return new CreateTaskResponseModel { TaskId = taskId };
        }
    }
}
