CREATE TABLE [dbo].[PracticalLessonEvent]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [PracticalLessonId] BIGINT NOT NULL, 
    [ProfessorId] BIGINT NOT NULL, 
    [DayOfWeek] NVARCHAR(20) NOT NULL, 
    [StartTime] TIME NOT NULL, 
    [EndTime] TIME NOT NULL, 
    [Occurance] TINYINT NULL, 
    [MaximumSize] TINYINT NULL, 
    CONSTRAINT [PK_PracticalLessonEvent] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_PracticalLessonEvent_PracticalLesson] FOREIGN KEY ([PracticalLessonId]) REFERENCES [PracticalLesson]([Id]), 
    CONSTRAINT [FK_PracticalLessonEvent_Professor] FOREIGN KEY ([ProfessorId]) REFERENCES [Professor]([Id]) 
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'An enum which describes the occurance. ex:1-weekly/2-bi-weekly',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PracticalLessonEvent',
    @level2type = N'COLUMN',
    @level2name = N'Occurance'