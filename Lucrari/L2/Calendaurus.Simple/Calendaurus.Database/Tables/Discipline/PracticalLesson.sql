CREATE TABLE [dbo].[PracticalLesson]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [DisciplineId] BIGINT NOT NULL, 
    [Type] BIGINT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_PracticalLesson] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_PracticalLesson_Discipline] FOREIGN KEY ([DisciplineId]) REFERENCES [Discipline]([Id]),
    CONSTRAINT [UK_PracticalLesson_DisciplineIdType] UNIQUE ([DisciplineId],[Type]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A short description on what the students will be doing at this practical lesson',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PracticalLesson',
    @level2type = N'COLUMN',
    @level2name = N'Description'