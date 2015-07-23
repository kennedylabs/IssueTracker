ALTER TABLE [dbo].[Issue]
	ADD CONSTRAINT [FK_Issue_AssignedToUser]
	FOREIGN KEY ([AssignedToUserId])
	REFERENCES [User] ([Id])
