ALTER TABLE [dbo].[Issue]
	ADD CONSTRAINT [FK_Issue_IssueType]
	FOREIGN KEY ([IssueTypeId])
	REFERENCES [IssueType] ([Id])
