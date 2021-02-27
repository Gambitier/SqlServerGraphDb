using MediatR;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.QueryResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerGraphDb.CommandQueryHandler.RequestModels.QueryRequestModels
{
    public class TaskDetailsRequestModel : IRequest<TaskDetailsResponseModel>
    {
        public int TaskId { get; set; }
    }
}
