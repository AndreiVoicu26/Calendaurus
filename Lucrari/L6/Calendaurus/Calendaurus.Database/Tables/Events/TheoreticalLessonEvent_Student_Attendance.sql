CREATE TABLE [dbo].[TheoreticalLessonEvent_Student_Attendance]
(
    [TheoreticalLessonEvenId] BIGINT NOT NULL, 
    [StudentId] BIGINT NOT NULL, 
    CONSTRAINT [FK_TheoreticalLessonEvent_Student_Attendance_TheoreticalLessonEvent] FOREIGN KEY ([TheoreticalLessonEvenId]) REFERENCES [TheoreticalLessonEvent]([Id]), 
    CONSTRAINT [FK_TheoreticalLessonEvent_Student_Attendance_Student] FOREIGN KEY ([StudentId]) REFERENCES [Student]([Id]), 
    CONSTRAINT [PK_TheoreticalLessonEvent_Student_Attendance] PRIMARY KEY ([TheoreticalLessonEvenId], [StudentId])
)
