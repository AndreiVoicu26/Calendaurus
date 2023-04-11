CREATE TABLE [dbo].[SystemAdmin]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    CONSTRAINT [FK_SystemAdmin_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [UK_SystemAdmin_UserId] UNIQUE([UserId]), 
    CONSTRAINT [PK_SystemAdmin] PRIMARY KEY ([Id])
)