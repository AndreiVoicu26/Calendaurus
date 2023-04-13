CREATE TABLE [dbo].[Professor]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [Title] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Professor] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Professor_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [UK_Professor_UserId] UNIQUE ([UserId]) 
)
