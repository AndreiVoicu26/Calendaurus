CREATE TABLE [dbo].[TheoreticalLessonEvent]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [TheoreticalLessonId] BIGINT NOT NULL, 
    [ProfessorId] BIGINT NOT NULL, 
    [DayOfWeek] TINYINT NOT NULL, 
    [StartTime] TIME NOT NULL, 
    [EndTime] TIME NOT NULL, 
    [Occurance] TINYINT NULL, 
    [MaximumSize] TINYINT NULL, 
    CONSTRAINT [PK_TheoreticalLessonEvent] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TheoreticalLessonEvent_PracticalLesson] FOREIGN KEY ([TheoreticalLessonId]) REFERENCES [TheoreticalLesson]([Id]),
    CONSTRAINT [FK_TheoreticalLessonEvent_Professor] FOREIGN KEY ([ProfessorId]) REFERENCES [Professor]([Id]) 
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'An enum which describes the occurance. ex:1-weekly/2-bi-weekly',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TheoreticalLessonEvent',
    @level2type = N'COLUMN',
    @level2name = N'Occurance'
