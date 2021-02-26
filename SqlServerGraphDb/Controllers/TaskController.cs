using MediatR;
using Microsoft.AspNetCore.Mvc;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using System;
using System.Threading.Tasks;

namespace SqlServerGraphDb.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewTaskAsync([FromBody] CreateTaskRequestModel requestModel)
        {
            // create new db entry, with taskName and current timeStamp
            // on successful entry, return id of db entry
            //------------------------Task Table----------------------------------
            // Id | taskName | createdOn | ExecutionStatus | ExecutionDescription |
            //---------------------------------------------------------------------
            var response = await _mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> UploadDataFileAsync([FromForm] UploadFileRequestModel requestModel)
        {
            // upload files and store information for
            // processing them later
            //--------TaskDataFile Table---------------------
            // Id | Task.Id | FileTypeEnum | FilePath |
            //-----------------------------------------
            var response = await _mediator.Send(requestModel).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllTaskDetails([FromQuery] string taskId)
        {
            // return data from TaskAndFiles table
            // group data with Task.Id
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult ExecuteTask([FromQuery] string taskId)
        {
            // Responsible for 
            // - reading all files uploaded for task
            // - validating csv file accorfing to format specified
            // - parse them 
            // - adding data to db in single transaction
            // - - -> add nodes related data to normal relational db table
            // - - -> add Node and NodeTypeEnum
            // - - -> add Edge according to Relation file
            // - - -> add EdgeId and RelationEnum to normal relational db table according to Relation file
            throw new NotImplementedException();
        }
    }
}
