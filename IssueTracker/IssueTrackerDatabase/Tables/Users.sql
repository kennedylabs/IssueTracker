CREATE TABLE [dbo].[Users] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (128) NOT NULL,
    [First]       NVARCHAR (128) NULL,
    [Last]        NVARCHAR (128) NULL,
    [Email]       NVARCHAR (128) NULL,
    [UserRole_Id] INT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRoleUser] FOREIGN KEY ([UserRole_Id]) REFERENCES [dbo].[UserRole] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_UserRoleUser]
    ON [dbo].[Users]([UserRole_Id] ASC);

