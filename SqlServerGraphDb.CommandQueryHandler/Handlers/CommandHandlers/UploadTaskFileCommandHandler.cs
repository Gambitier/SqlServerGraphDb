using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using SqlServerGraphDb.Persistence.Persistence;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SqlServerGraphDb.CommandQueryHandler.Handlers.CommandHandlers
{
    public class UploadTaskFileCommandHandler : IRequestHandler<UploadFileRequestModel, UploadFileResponseModel>
    {
        private readonly ITaskPersistence _taskPersistence;
        private readonly IWebHostEnvironment _hostingEnvironment;
        IConfiguration _configuration;

        public UploadTaskFileCommandHandler(
            ITaskPersistence taskPersistence,
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _taskPersistence = taskPersistence;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<UploadFileResponseModel> Handle(UploadFileRequestModel request, CancellationToken cancellationToken)
        {
            bool response = false;
            if (Path.GetExtension(request.File.FileName).ToLowerInvariant() == "csv")
            {
                //get shouldUploadFile value from database
                //create sp 
                // -if taskId is valid
                // ---then add/edit filename to TaskDataFile table with all detail and return true
                // -else
                //---return false
                var shouldUploadFile = true;
                response = shouldUploadFile;

                if (shouldUploadFile)
                {
                    var pathToSaveFile = Path.Combine(
                        _hostingEnvironment.ContentRootPath,
                        _configuration["TaskUploadsFolderName"],
                        $"{request.TaskId}");

                    if (!Directory.Exists(pathToSaveFile))
                    {
                        Directory.CreateDirectory(pathToSaveFile);
                    }

                    using FileStream stream = new FileStream(Path.Combine(pathToSaveFile, request.File.FileName), FileMode.Create);
                    request.File.CopyTo(stream);
                }
            }

            return new UploadFileResponseModel { Success = response };
        }
    }
}
