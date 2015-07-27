ALTER TABLE [dbo].[Issue]
	ADD CONSTRAINT [FK_Issue_CreatedByUser]
	FOREIGN KEY ([CreatedByUserId])
	REFERENCES [User] ([Id])
