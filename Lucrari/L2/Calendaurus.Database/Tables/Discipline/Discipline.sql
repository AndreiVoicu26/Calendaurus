CREATE TABLE [dbo].[Discipline]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [Year] TINYINT NOT NULL, 
    [Faculty] NVARCHAR(200) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Discipline] PRIMARY KEY ([Id])
)
