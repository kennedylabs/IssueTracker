CREATE TABLE [dbo].[Issue] (
    [Id] INT IDENTITY,
    [IssueTypeId] INT NOT NULL,
    [IssueStatusId] INT NOT NULL,
    [IssuePriorityId] INT NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [AssignedToUserId] INT NOT NULL,
    [Name] NVARCHAR(128) NOT NULL,
    [Summary] NVARCHAR(MAX) NOT NULL,
	[SubSystem] NVARCHAR(128) NOT NULL,
	[Customer] NVARCHAR(128) NULL,
	[Estimate] INT NULL,
	[ReportedOn] DATETIMEOFFSET(7) NOT NULL,
    [CreatedOn] DATETIMEOFFSET(7) NOT NULL,
    [ModifiedOn] DATETIMEOFFSET(7) NOT NULL,
    CONSTRAINT [PK_Issue] PRIMARY KEY ([Id]),
)
GO

CREATE INDEX [IX_Issue_CreatedByUserId]
	ON [dbo].[Issue] ([CreatedByUserId])
GO

CREATE INDEX [IX_Issue_AssignedToUserId]
	ON [dbo].[Issue] ([AssignedToUserId])
GO

CREATE INDEX [IX_Issue_IssueTypeId]
	ON [dbo].[Issue] ([IssueTypeId])
GO

CREATE INDEX [IX_Issue_IssueStatusId]
	ON [dbo].[Issue] ([IssueStatusId])
GO

CREATE INDEX [IX_Issue_IssuePriorityId]
	ON [dbo].[Issue] ([IssuePriorityId])
