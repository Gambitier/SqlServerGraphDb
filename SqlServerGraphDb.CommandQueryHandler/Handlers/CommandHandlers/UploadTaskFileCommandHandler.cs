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
        private readonly ITaskMetadataPersistence _taskMetadataPersistence;
        private readonly IWebHostEnvironment _hostingEnvironment;
        IConfiguration _configuration;

        public UploadTaskFileCommandHandler(
            ITaskMetadataPersistence taskMetadataPersistence,
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _taskMetadataPersistence = taskMetadataPersistence;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<UploadFileResponseModel> Handle(UploadFileRequestModel request, CancellationToken cancellationToken)
        {
            bool response = false;
            if (Path.GetExtension(request.File.FileName).ToLowerInvariant() == ".csv")
            {
                response = await _taskMetadataPersistence.UploadTaskFile(request.TaskId, (int)request.FileType, request.File.FileName);

                if (response)
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
