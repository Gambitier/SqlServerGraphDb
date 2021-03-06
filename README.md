
# PoC implementation of importing CSV data to graph db.

* SQL script for creating database can be found [here](https://github.com/Gambitier/SqlServerGraphDb/tree/master/SqlServerGraphDb.Persistence/DatabaseDump)
* Sample files to upload can be found [here](https://github.com/Gambitier/SqlServerGraphDb/tree/master/SqlServerGraphDb/Content/Sample%20files%20for%20upload)

Steps:

1. Create a task by providing task name, this will return int task id
2. Use this task id, for following operations

	-  GetTaskDetail: Returns list of names of files uploaded
	-  Upload data files: [sample files](https://github.com/Gambitier/SqlServerGraphDb/tree/master/SqlServerGraphDb/Content/Sample%20files%20for%20upload), use following enums
	
		- File Type Enums:
		
			- Job = 1,

			- Operation = 2,

			- Project = 3,

			- Relation = 4

	- ExecuteTask: Parses the csv files and upload data first into relational database and then to graph database tables

---

