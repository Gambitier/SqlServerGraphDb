USE [GraphDBDemo]
GO

/****** Object:  StoredProcedure [dbo].[spTaskMetadata_GetAllByTaskId]    Script Date: 28-02-2021 12.33.07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTaskMetadata_GetAllByTaskId]
		@TaskId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from [dbo].[TaskMetadata] where TaskId = @TaskId;
END
GO


