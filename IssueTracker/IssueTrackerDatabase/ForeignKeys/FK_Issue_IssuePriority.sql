ALTER TABLE [dbo].[Issue]
	ADD CONSTRAINT [FK_Issue_IssuePriority]
	FOREIGN KEY ([IssuePriorityId])
	REFERENCES [IssuePriority] ([Id])
