ALTER TABLE [dbo].[Comment]
	ADD CONSTRAINT [FK_Comment_Issue]
	FOREIGN KEY ([IssueId])
	REFERENCES [dbo].[Issue] ([Id])
