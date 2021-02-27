using MediatR;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.QueryRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.QueryResponseModels;
using SqlServerGraphDb.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlServerGraphDb.CommandQueryHandler.Handlers.QueryHandlers
{
    public class GetTaskMetadataDetailsQueryHandler : IRequestHandler<TaskDetailsRequestModel, TaskDetailsResponseModel>
    {
        private readonly ITaskMetadataPersistence _taskMetadataPersistence;

        public GetTaskMetadataDetailsQueryHandler(ITaskMetadataPersistence taskMetadataPersistence)
        {
            _taskMetadataPersistence = taskMetadataPersistence;
        }

        public async Task<TaskDetailsResponseModel> Handle(TaskDetailsRequestModel request, CancellationToken cancellationToken)
        {
            var data = await _taskMetadataPersistence.GetTaskMetadataByTaskId(request.TaskId);
            var response = new TaskDetailsResponseModel { TaskDetails = data };
            return response;
        }
    }
}
