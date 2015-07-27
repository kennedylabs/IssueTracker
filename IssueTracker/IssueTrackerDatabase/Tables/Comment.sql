CREATE TABLE [dbo].[Comment]
(
	[Id] INT IDENTITY,
    [UserId] INT NOT NULL,
    [IssueId] INT NOT NULL,
    [Text] NVARCHAR(MAX) NOT NULL,
    [CreatedOn] DATETIMEOFFSET NOT NULL,
    [ModifiedOn] DATETIMEOFFSET NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY ([Id])
)
GO

CREATE INDEX [IX_Comment_UserId]
	ON [dbo].[Comment] ([UserId])
GO

CREATE INDEX [IX_Comment_IssueId]
	ON [dbo].[Comment] ([IssueId])
