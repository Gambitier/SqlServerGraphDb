USE [GraphDBDemo]
GO

/****** Object:  Table [dbo].[TaskMetadata]    Script Date: 27-02-2021 11.38.54 PM ******/
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

ALTER TABLE [dbo].[TaskMetadata] ADD  CONSTRAINT [DF_TaskMetadata_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[TaskMetadata] ADD  CONSTRAINT [DF_TaskMetadata_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO

ALTER TABLE [dbo].[TaskMetadata]  WITH CHECK ADD  CONSTRAINT [FK_TaskMetadata_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO

ALTER TABLE [dbo].[TaskMetadata] CHECK CONSTRAINT [FK_TaskMetadata_Task]
GO


