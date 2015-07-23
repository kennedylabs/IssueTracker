ALTER TABLE [dbo].[Comment]
	ADD CONSTRAINT [FK_Comment_User]
	FOREIGN KEY ([UserId])
	REFERENCES [User] ([Id])
