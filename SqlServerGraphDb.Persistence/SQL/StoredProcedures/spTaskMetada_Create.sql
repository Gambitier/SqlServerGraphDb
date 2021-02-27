USE [GraphDBDemo]
GO

/****** Object:  StoredProcedure [dbo].[spTaskMetada_Create]    Script Date: 27-02-2021 10.29.43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spTaskMetada_Create]
	@TaskId int,
	@FileType smallint,
	@FileName varchar(50),
	@FileProcessingStatus smallint,
	@NewRecordInserted BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS (select Id
				   from [dbo].[TaskMetadata]
				   where 
						TaskId = @TaskId 
						and FileType = @FileType)
		BEGIN
			BEGIN TRY
				INSERT INTO [dbo].[TaskMetadata]
				   ([TaskId]
				   ,[FileType]
				   ,[FileName]
				   ,[FileProcessingStatus])
				 VALUES
					(@TaskId
					,@FileType
					,@FileName
					,@FileProcessingStatus);

				set @NewRecordInserted = 1;
			END TRY
			BEGIN CATCH
				set @NewRecordInserted = 0;
			END CATCH
		END
	ELSE
		BEGIN
			set @NewRecordInserted = 0;
		END
END
GO


