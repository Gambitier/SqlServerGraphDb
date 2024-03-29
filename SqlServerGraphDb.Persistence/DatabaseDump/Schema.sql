USE [master]
GO
/****** Object:  Database [GraphDBDemo]    Script Date: 04-03-2021 10.09.20 PM ******/
CREATE DATABASE [GraphDBDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GraphDBDemo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\GraphDBDemo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GraphDBDemo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\GraphDBDemo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [GraphDBDemo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GraphDBDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GraphDBDemo] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [GraphDBDemo] SET ANSI_NULLS ON 
GO
ALTER DATABASE [GraphDBDemo] SET ANSI_PADDING ON 
GO
ALTER DATABASE [GraphDBDemo] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [GraphDBDemo] SET ARITHABORT ON 
GO
ALTER DATABASE [GraphDBDemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GraphDBDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GraphDBDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GraphDBDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GraphDBDemo] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [GraphDBDemo] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [GraphDBDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GraphDBDemo] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [GraphDBDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GraphDBDemo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GraphDBDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GraphDBDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GraphDBDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GraphDBDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GraphDBDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GraphDBDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GraphDBDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GraphDBDemo] SET RECOVERY FULL 
GO
ALTER DATABASE [GraphDBDemo] SET  MULTI_USER 
GO
ALTER DATABASE [GraphDBDemo] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [GraphDBDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GraphDBDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GraphDBDemo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GraphDBDemo] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'GraphDBDemo', N'ON'
GO
ALTER DATABASE [GraphDBDemo] SET QUERY_STORE = OFF
GO
USE [GraphDBDemo]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[DataId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
	[Title] [nchar](100) NOT NULL,
	[Customer] [nchar](100) NOT NULL,
	[Detail] [ntext] NULL,
	[Summary] [ntext] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Job] UNIQUE NONCLUSTERED 
(
	[DataId] ASC,
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NodeAndType]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NodeAndType](
	[Id] [uniqueidentifier] NULL,
	[TypeId] [int] NULL,
	[DataId] [int] NULL,
	[TaskId] [int] NULL
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NodeRelation]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NodeRelation](
	[RelationId] [int] NULL
)
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[DataId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
	[Title] [nchar](100) NOT NULL,
	[Customer] [nchar](100) NOT NULL,
	[Detail] [ntext] NULL,
	[Summary] [ntext] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Operation] UNIQUE NONCLUSTERED 
(
	[DataId] ASC,
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[DataId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
	[Title] [nchar](100) NOT NULL,
	[Customer] [nchar](100) NOT NULL,
	[Detail] [ntext] NULL,
	[Summary] [ntext] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Project] UNIQUE NONCLUSTERED 
(
	[DataId] ASC,
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Relation]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relation](
	[TaskId] [int] NOT NULL,
	[FromDataId] [int] NOT NULL,
	[FromTableEnum] [int] NOT NULL,
	[ToDataId] [int] NOT NULL,
	[ToTableEnum] [int] NOT NULL,
	[RelationEnum] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Relation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskMetadata]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[FileType] [smallint] NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[FileProcessingStatus] [smallint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_TaskMetadata] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TaskFile] UNIQUE NONCLUSTERED 
(
	[TaskId] ASC,
	[FileType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [GRAPH_UNIQUE_INDEX_DE34E7D0ACAD46A6BAB1DCC83FCBCCB1]    Script Date: 04-03-2021 10.09.21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [GRAPH_UNIQUE_INDEX_DE34E7D0ACAD46A6BAB1DCC83FCBCCB1] ON [dbo].[NodeAndType]
(
	$node_id
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [GRAPH_UNIQUE_INDEX_41B776F5640443FD8099077EFC153935]    Script Date: 04-03-2021 10.09.21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [GRAPH_UNIQUE_INDEX_41B776F5640443FD8099077EFC153935] ON [dbo].[NodeRelation]
(
	$edge_id
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Job] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Operation] ADD  CONSTRAINT [DF_Operation_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Operation] ADD  CONSTRAINT [DF_Operation_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Operation] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Relation] ADD  CONSTRAINT [DF_Relation_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Relation] ADD  CONSTRAINT [DF_Relation_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DF_Task_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[TaskMetadata] ADD  CONSTRAINT [DF_TaskMetadata_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[TaskMetadata] ADD  CONSTRAINT [DF_TaskMetadata_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK ADD  CONSTRAINT [FK_Job_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Task]
GO
ALTER TABLE [dbo].[Operation]  WITH NOCHECK ADD  CONSTRAINT [FK_Operation_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Task]
GO
ALTER TABLE [dbo].[Project]  WITH NOCHECK ADD  CONSTRAINT [FK_Project_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Task]
GO
ALTER TABLE [dbo].[TaskMetadata]  WITH CHECK ADD  CONSTRAINT [FK_TaskMetadata_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[TaskMetadata] CHECK CONSTRAINT [FK_TaskMetadata_Task]
GO
/****** Object:  StoredProcedure [dbo].[spTask_CreateTaskByName]    Script Date: 04-03-2021 10.09.21 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spTaskMetada_Create]    Script Date: 04-03-2021 10.09.21 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spTaskMetadata_GetAllByTaskId]    Script Date: 04-03-2021 10.09.21 PM ******/
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
/****** Object:  Trigger [dbo].[tr_AfterInsertAddNode]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[tr_AfterInsertAddNode]
ON [dbo].[Job]
AFTER INSERT
AS
BEGIN	
	INSERT INTO dbo.NodeAndType (Id, TypeId,DataId,TaskId)
	select Id, 1, DataId,TaskId
	from inserted;

END
GO
ALTER TABLE [dbo].[Job] ENABLE TRIGGER [tr_AfterInsertAddNode]
GO
/****** Object:  Trigger [dbo].[tr_AfterInsertAddOperationNode]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[tr_AfterInsertAddOperationNode]
ON [dbo].[Operation]
AFTER INSERT
AS
BEGIN
	--insert id of Operation table and type as 1(as enum for Operation table)
    DECLARE @InsertedRecordId UNIQUEIDENTIFIER
	
	INSERT INTO dbo.NodeAndType (Id, TypeId,DataId,TaskId)
	select Id, 2, DataId,TaskId
	from inserted;

END
GO
ALTER TABLE [dbo].[Operation] ENABLE TRIGGER [tr_AfterInsertAddOperationNode]
GO
/****** Object:  Trigger [dbo].[tr_AfterInsertAddProjectNode]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[tr_AfterInsertAddProjectNode]
ON [dbo].[Project]
AFTER INSERT
AS
BEGIN
	--insert id of Project table and type as 1(as enum for Project table)
    DECLARE @InsertedRecordId UNIQUEIDENTIFIER
	
	INSERT INTO dbo.NodeAndType (Id, TypeId,DataId,TaskId)
	select Id, 3, DataId,TaskId
	from inserted;

END
GO
ALTER TABLE [dbo].[Project] ENABLE TRIGGER [tr_AfterInsertAddProjectNode]
GO
/****** Object:  Trigger [dbo].[tr_AfterInsertUpdateRelationEdgeTable]    Script Date: 04-03-2021 10.09.21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[tr_AfterInsertUpdateRelationEdgeTable] 
   ON  [dbo].[Relation] 
   AFTER Insert
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[NodeRelation]
	select 
		(SELECT $node_id FROM [dbo].[NodeAndType] 
			WHERE TypeId=inserted.FromTableEnum and 
			      DataId=inserted.FromDataId and 
				  TaskId=inserted.TaskId),
		
		(SELECT $node_id FROM [dbo].[NodeAndType] 
			WHERE TypeId=inserted.ToTableEnum and 
			      DataId=inserted.ToDataId and 
				  TaskId=inserted.TaskId),

		inserted.RelationEnum
	from inserted;

END
GO
ALTER TABLE [dbo].[Relation] ENABLE TRIGGER [tr_AfterInsertUpdateRelationEdgeTable]
GO
USE [master]
GO
ALTER DATABASE [GraphDBDemo] SET  READ_WRITE 
GO
