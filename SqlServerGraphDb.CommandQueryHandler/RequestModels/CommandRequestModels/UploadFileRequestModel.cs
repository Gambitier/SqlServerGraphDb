using MediatR;
using Microsoft.AspNetCore.Http;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using static SqlServerGraphDb.Persistence.Enums.Enums;

namespace SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels
{
    public class UploadFileRequestModel : IRequest<UploadFileResponseModel>
    {
        public int TaskId { get; set; }
        public FileType FileType { get; set; }
        public IFormFile File { get; set; }
    }
}
