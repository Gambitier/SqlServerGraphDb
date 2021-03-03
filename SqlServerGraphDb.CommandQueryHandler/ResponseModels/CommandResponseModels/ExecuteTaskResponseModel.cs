using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels
{
    public class ExecuteTaskResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
