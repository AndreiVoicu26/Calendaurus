CREATE TABLE [dbo].[Student]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [YearId] BIGINT NOT NULL, 
    [DateOfBirth] DATE NOT NULL,  
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Student_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [UK_Student_UserId] UNIQUE ([UserId]), 
    CONSTRAINT [FK_Student_Year] FOREIGN KEY ([YearId]) REFERENCES [Year]([Id])
)
