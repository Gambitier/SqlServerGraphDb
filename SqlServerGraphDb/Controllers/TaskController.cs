using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerGraphDb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        enum ExecutionStatus
        {
            Success = 1, //processed all files and data inserted to db successfully
            Failed = 2, //Some error occured, =fileMissing, =invalidFileFormat-filetype
        }

        [HttpPost]
        public IActionResult CreateNewTask(string taskName)
        {
            // create new db entry, with taskName and current timeStamp
            // on successful entry, return id of db entry
            //------------------------Task Table----------------------------------
            // Id | taskName | createdOn | ExecutionStatus | ExecutionDescription |
            //---------------------------------------------------------------------

            throw new NotImplementedException();
        }

        enum FileType
        {
            Job = 1,
            Operation = 2,
            Project = 3,
            Relation = 4
        }

        [HttpPost]
        public IActionResult UploadDataFile(string file, int fileType)
        {
            // upload files and store information for
            // processing them later
            //--------TaskAndFile Table---------------------
            // Id | Task.Id | FileTypeEnum | FilePath |
            //-----------------------------------------

            FileType fileTypeEnum = (FileType)fileType;
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetAllTaskDetails(string taskId)
        {
            // return data from TaskAndFiles table
            // group data with Task.Id
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult ExecuteTask(string taskId)
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
