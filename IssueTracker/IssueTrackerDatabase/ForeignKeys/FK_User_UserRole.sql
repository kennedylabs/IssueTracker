﻿ALTER TABLE [dbo].[User]
	ADD CONSTRAINT [FK_User_UserRole]
	FOREIGN KEY ([UserRoleId])
	REFERENCES [UserRole] ([Id])
