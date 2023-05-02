CREATE TABLE [dbo].[SysAdmin]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    CONSTRAINT [PK_SysAdmin] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_SysAdmin_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [UK_SysAdmin_UserId] UNIQUE ([UserId]) 
)
