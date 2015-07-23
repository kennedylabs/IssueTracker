CREATE TABLE [dbo].[User] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [UserRoleId] INT NOT NULL,
    [UserName] NVARCHAR (128) NOT NULL,
    [First] NVARCHAR (128) NULL,
    [Last] NVARCHAR (128) NULL,
    [Email] NVARCHAR (128) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
)

GO
CREATE INDEX [IX_UserRoleId]
ON [dbo].[User] ([UserRoleId])
