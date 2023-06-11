import { Grid } from "@mui/material";
import styled from "styled-components";
import {
  Discipline,
  PracticalLesson,
  PracticalLessonEvent,
} from "../../actions/models";
import { convertDayOfWeek, convertTimespanToHour } from "../../actions/utils";
import { PracticalLessonItem } from "./PracticalLessonItem";

const Root = styled.div`
  display: block;
  background-color: white;
  color: black;
  width: 24rem;
  height: fit-content;
  border: 0.1rem solid black;
`;

const Hour = styled.div`
  display: block;
  padding: 19px;
  font-size: 20px;
  background-color: #EAEBF5;
`;

const ColumnHeader = styled.div`
  display: block;
  padding: 19px;
  background-color: #A5A9C5;
  font-size: 20px;
`;

export interface DayColumnProps {
  day: string;
  practicalLessons: PracticalLesson[];
  enrollments: PracticalLesson[];
  studentDisciplines: Discipline[];
  updateIsEnrolled: (enrolled?: boolean) => void;
}

const getPracticalLessonEvent = (
  practicalLessonEvents: PracticalLessonEvent[],
  hour: number,
  day: string
) =>
  practicalLessonEvents?.find(
    (ple) =>
      convertTimespanToHour(ple.startTime) === hour &&
      convertDayOfWeek(ple.dayOfWeek) === day
  );

export function DayColumnComponent(props: DayColumnProps) {
  const rows = [];
  if (props.practicalLessons.length > 0) {
    for (let i = 8; i < 20; i++) {
      let practicalLesson: PracticalLesson | undefined =
        props.practicalLessons.find((pl) =>
          getPracticalLessonEvent(pl.practicalLessonEvents, i, props.day)
        );

      if (practicalLesson) {
        const practicalLessonEvent = getPracticalLessonEvent(
          practicalLesson.practicalLessonEvents,
          i,
          props.day
        );
        practicalLesson = practicalLessonEvent
          ? {
              ...practicalLesson,
              practicalLessonEvents: [practicalLessonEvent],
            }
          : undefined;
      }

      const isEnrolled =
        props.enrollments?.findIndex((x) => x.id === practicalLesson?.id) > -1;
      const disciplineName: string | undefined = props.studentDisciplines.find(
        (sd) => sd.id === practicalLesson?.disciplineId
      )?.name;
      rows.push(
        <Grid
          item
          xs={8}
          key={props.day + i.toString()}
          sx={{ borderBottom: "0.1rem solid black", width: "24rem" }}
        >
          <Hour key={`hour-${i}`}>
            {!practicalLesson ? (
              <div key={`empty-lesson-${i}`}>&nbsp;</div>
            ) : (
              <PracticalLessonItem
                key={`practical-lesson-${i}`}
                practicalLessonDescription={practicalLesson?.description}
                practicalLessonEvent={
                  practicalLesson?.practicalLessonEvents?.[0]
                }
                disciplineName={disciplineName}
                isEnrolled={isEnrolled}
                updateIsEnrolled={props.updateIsEnrolled}
              />
            )}
          </Hour>
        </Grid>
      );
    }
  }
  return (
    <Root>
      <Grid
        container
        spacing={0}
        direction="column"
        justifyContent="space-evenly"
        alignItems="center"
      >
        <Grid
          item
          xs={8}
          sx={{
            borderBottomStyle: "solid",
            width: "24rem",
            textAlign: "center",
          }}
        >
          <ColumnHeader>{props.day}</ColumnHeader>
        </Grid>
        {rows}
      </Grid>
    </Root>
  );
}

export const DayColumn = DayColumnComponent;
