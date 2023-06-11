CREATE TABLE [dbo].[TheoreticalLesson]
(
    [Id] BIGINT NOT NULL IDENTITY,
	[DisciplineId] BIGINT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_TheoreticalLesson_Discipline] FOREIGN KEY ([DisciplineId]) REFERENCES [Discipline]([Id]),
    CONSTRAINT [PK_TheoreticalLesson] PRIMARY KEY ([Id]), 
    CONSTRAINT [UK_TheoreticalLesson_DisciplineId] UNIQUE ([DisciplineId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A short description on what the students will be learning at this theoretical lesson',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TheoreticalLesson',
    @level2type = N'COLUMN',
    @level2name = N'Description'