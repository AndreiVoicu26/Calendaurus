export interface Discipline {
  id: number;
  year: number;
  faculty: string;
  name: string;
}

export interface PracticalLesson {
  id: number;
  disciplineId: number;
  type: number;
  description: string;
  practicalLessonEvents: PracticalLessonEvent[];
}

export interface PracticalLessonEvent {
  id: number;
  practicalLessonId: number;
  dayOfWeek: string;
  startTime: string;
  endTime: string;
  occurance: number;
  maximumSize: number;
}

export interface PracticalLessonEventEnrollment {
  practicalLessonEventId: number;
}