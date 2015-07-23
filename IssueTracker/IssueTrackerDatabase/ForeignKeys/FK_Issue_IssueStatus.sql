ALTER TABLE [dbo].[Issue]
	ADD CONSTRAINT [FK_Issue_IssueStatus]
	FOREIGN KEY ([IssueStatusId])
	REFERENCES [IssueStatus] ([Id])
