CREATE TABLE [dbo].[Discipline]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [YearId] BIGINT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Discipline] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Discipline_Year] FOREIGN KEY ([YearId]) REFERENCES [Year]([Id]) 
)
