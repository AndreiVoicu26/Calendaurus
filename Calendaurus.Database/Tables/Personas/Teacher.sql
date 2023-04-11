CREATE TABLE [dbo].[Teacher]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Teacher] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Teacher_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [AK_Teacher_UserId] UNIQUE ([UserId]) 
)
