USE [GraphDBDemo]
GO

/****** Object:  StoredProcedure [dbo].[spTask_CreateTaskByName]    Script Date: 27-02-2021 10.29.18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTask_CreateTaskByName]
	@Name varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[Task]([Name])
	VALUES(@Name)
	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO


