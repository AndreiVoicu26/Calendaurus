import { Box, Grid, Paper } from "@mui/material";
import styled from "styled-components";
import * as React from "react";
import { Discipline, PracticalLesson } from "../../actions/models";
import { convertDayOfWeek, isNullOrUndefined } from "../../actions/utils";
import moment from "moment";

const Root = styled.div`
  display: block;
  background-color: white;
  color: black;
  width: 22rem;
  height: fit-content;
  border: 0.1rem solid gray;
`;

const Hour = styled.div`
  display: block;
  padding: 1rem;
`;

const ColumnHeader = styled.div`
  display: block;
  padding: 1rem;
  background-color: #f5f5f5;
`;

export interface DayColumnProps {
  day: string;
  practicalLessons: PracticalLesson[];
  studentDisciplines: Discipline[];
}

export function DayColumnComponent(props: DayColumnProps) {
  const rows = [];
  if (props.practicalLessons.length > 0 && props.practicalLessons.length > 0) {
    for (let i = 8; i < 20; i++) {
      const practicalLesson: PracticalLesson | undefined =
        props.practicalLessons.find(
          (pl) =>
            !isNullOrUndefined(
              pl.practicalLessonEvents?.find(
                (ple) =>
                  moment(ple.startTime, "HH:mm:ss").hour() === i &&
                  convertDayOfWeek(ple.dayOfWeek) === props.day
              )
            )
        );
      const disciplineName: string | undefined = props.studentDisciplines.find(
        (sd) => sd.id === practicalLesson?.disciplineId
      )?.name;
      rows.push(
        <Grid
          item
          xs={8}
          key={props.day + i.toString()}
          sx={{ borderBottom: "0.1rem solid black", width: "22rem" }}
        >
          <Hour>
            {isNullOrUndefined(practicalLesson) ? (
              <>&nbsp;</>
            ) : (
              <Box>
                <Paper
                  sx={{
                    backgroundColor: "white",
                    padding: "0rem 0.5rem",
                    "&:hover": {
                      backgroundColor: "#f0f0f0",
                    },
                  }}
                  elevation={3}
                >
                  {disciplineName}: {practicalLesson?.description}
                </Paper>
              </Box>
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
            borderBottomStyle: "double",
            width: "22rem",
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
