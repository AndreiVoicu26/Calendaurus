CREATE TABLE [dbo].[Student]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [Year] TINYINT NOT NULL, 
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Student_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [UK_Student_UserId] UNIQUE ([UserId]), 
)
