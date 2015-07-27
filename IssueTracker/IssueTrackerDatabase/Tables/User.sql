CREATE TABLE [dbo].[User] (
    [Id] INT IDENTITY NOT NULL,
    [UserRoleId] INT NOT NULL,
    [UserName] NVARCHAR(128) NOT NULL,
    [First] NVARCHAR(128) NULL,
    [Last] NVARCHAR(128) NULL,
    [Email] NVARCHAR(128) NULL,
    [PasswordHash] NVARCHAR(128) NULL,
    [PasswordSalt] NVARCHAR(128) NULL,
    [PasswordLength] INT NULL,
    [AuthToken] NVARCHAR(128) NULL,
    [AuthExpiration] DATETIMEOFFSET(7) NULL,
	[IsDeleted] BIT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
)
GO

CREATE INDEX [IX_UserRoleId]
	ON [dbo].[User] ([UserRoleId])
