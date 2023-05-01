CREATE TABLE [dbo].[PracticalLessonEvent_Student_Attendance]
(
    [PracticalLessonEventId] BIGINT NOT NULL, 
    [StudentId] BIGINT NOT NULL, 
    CONSTRAINT [FK_PracticalLessonEvent_Student_Attendance_PracticalLessonEvent] FOREIGN KEY ([PracticalLessonEventId]) REFERENCES [PracticalLessonEvent]([Id]), 
    CONSTRAINT [FK_PracticalLessonEvent_Student_Attendance_Student] FOREIGN KEY ([StudentId]) REFERENCES [Student]([Id]), 
    CONSTRAINT [PK_PracticalLessonEvent_Student_Attendance] PRIMARY KEY ([PracticalLessonEventId], [StudentId]) 
)
