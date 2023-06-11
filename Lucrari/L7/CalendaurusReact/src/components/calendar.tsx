import styled from "styled-components";
import { Button } from "./button";
import { useMsal } from "@azure/msal-react";
import * as React from "react";
import { Discipline, PracticalLesson } from "../actions/models";
import { TimeColumn } from "./Calendar/TimeColumn";
import { DayColumn } from "./Calendar/DayColumn";
import * as loaders from "../actions/api";

const Root = styled.div`
  display: grid;
  grid-template-rows: max-content minmax(0, 1fr);
  position: absolute;
  inset: 0;
`;
const Header = styled.div`
  align-items: center;
  display: grid;
  gap: 0.5rem;
  grid-template-columns: max-content 1fr max-content;
  padding: 0.5rem;
  border-bottom: 1px solid mediumpurple;
`;

export interface AuthenticatedProps {
  onSignOut: () => void;
}

const Content = styled.div`
  overflow: auto;
  display: flex;
  tr {
    height: 2rem;
    border-bottom: 1px solid mediumpurple;
  }
`;

export interface CalendarState {
  disciplines: Discipline[];
  studentDisciplines: Discipline[];
  practicalLessons: PracticalLesson[];
  enrollments: PracticalLesson[];
}

const weekdays: string[] = [
  "Monday",
  "Tuesday",
  "Wednesday",
  "Thursday",
  "Friday",
];

export function Calendar(props: AuthenticatedProps) {
  const { onSignOut } = props;

  const { instance } = useMsal();
  const [currentState, setCurrentState] = React.useState<CalendarState>({
    disciplines: [],
    studentDisciplines: [],
    practicalLessons: [],
    enrollments: [],
  });

  const loadStudentEnrollments = React.useCallback( () => {
    loaders.loadStudentEnrollments(
      instance,
      (enrollments: PracticalLesson[]) => {
        setCurrentState((prevState) => {
          return {
            ...prevState,
            enrollments,
          };
        });
      }
    );
  }, [instance]);

  React.useEffect(() => {
    loaders.loadDisciplines(instance, (disciplines: Discipline[]) => {
      setCurrentState((prevState) => {
        return {
          ...prevState,
          disciplines,
        };
      });
    });
    loaders.loadStudentDisciplines(
      instance,
      (studentDisciplines: Discipline[]) => {
        setCurrentState((prevState) => {
          return {
            ...prevState,
            studentDisciplines,
          };
        });
      }
    );
    loaders.loadStudentPractical(
      instance,
      (practicalLessons: PracticalLesson[]) => {
        setCurrentState((prevState) => {
          return {
            ...prevState,
            practicalLessons,
          };
        });
      }
    );
    loadStudentEnrollments();
  }, [instance, loadStudentEnrollments]);

  const weekdaysColumns = weekdays.map((day) => {
    return (
      <DayColumn
        day={day}
        practicalLessons={currentState.practicalLessons}
        studentDisciplines={currentState.studentDisciplines}
        enrollments={currentState.enrollments}
        updateIsEnrolled={loadStudentEnrollments}
      />
    );
  });

  return (
    <Root>
      <Header>
        Calendar
        <span></span>
        <Button onClick={onSignOut}>Log Out</Button>
      </Header>
      <Content>
        <TimeColumn />
        {weekdaysColumns}
      </Content>
    </Root>
  );
}
