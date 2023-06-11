import { Grid } from "@mui/material";
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import {
  Discipline,
  PracticalLesson,
  PracticalLessonEvent,
} from "../../actions/models";
import styled from "styled-components";
import * as React from "react";

const Root = styled.div`
  display: block;
  background-color: white;
  color: black;
  width: fit-content;
  height: fit-content;
  border: 0.1rem solid black;
`;

const Hour = styled.div`
  display: block;
  padding: 19px;
  background-color: #A5A9C5;
  text-align: center;
  font-size: 20px;
`;

const Cell = styled.div`
  display: block;
  padding: 16px 0px;
  background-color: #A5A9C5;
  text-align: center;
  font-size: 10px;
`;

type Anchor = 'left';

export interface DrawerProps{
  studentDisciplines: Discipline[];
  enrollments: PracticalLesson[];
}

export function TimeColumnComponent(props: DrawerProps) {
  const rows = [];
  for (let i = 8; i < 20; i++) {
    rows.push(
      <Grid
        item
        xs={8}
        key={i.toString()}
        sx={{ borderBottom: "0.1rem solid black", width: "7rem"}}
      >
        <Hour>{i.toString() + " - " + (i + 1).toString()}</Hour>
      </Grid>
    );
  }

  const [state, setState] = React.useState({
    left: false,
  });

  const toggleDrawer =
    (anchor: Anchor, open: boolean) =>
    (event: React.KeyboardEvent | React.MouseEvent) => {
      if (
        event.type === 'keydown' &&
        ((event as React.KeyboardEvent).key === 'Tab' ||
          (event as React.KeyboardEvent).key === 'Shift')
      ) {
        return;
      }

      setState({ ...state, [anchor]: open });
    };

  const list = (anchor: Anchor) => (
    <Box
      sx={{ 
        width: 500,
        padding: "1rem",
        fontSize: "25px",
      }}
      role="application"
    >
      <span>My enrollments:</span>
      <List sx={{borderTop: "black solid 0.2rem"}}>
        {props.enrollments.map((enrollment, index) => (
          <ListItem 
            key={enrollment.id} 
            sx={{
              fontSize: "20px",
              borderBottom: "black solid 0.1rem",
            }}>
            <span>
              {props.studentDisciplines.find((sd) => sd.id == enrollment?.disciplineId)?.name} - {enrollment.description}
              : {enrollment.practicalLessonEvents.find((ple)=> ple.practicalLessonId === enrollment.id)?.startTime} - {enrollment.practicalLessonEvents.find((ple)=> ple.practicalLessonId === enrollment.id)?.endTime}
            </span>
          </ListItem>
        ))}
      </List>

    </Box>
  );

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
            width: "7rem",
          }}
        >
          <Cell>
              {(['left'] as const).map((anchor) => (
                <React.Fragment key={anchor}>
                  <Button 
                    onClick={toggleDrawer(anchor, true)} 
                    sx={{
                      padding: "6px 0",
                      cursor: "pointer",
                      borderRadius: "10px",
                      background: "#6672CB",
                      color: "black",
                      ':hover': {
                        backgroundColor: "#0C1137",
                        color: "white"
                      }
                    }}
                  >
                    Enrollments
                  </Button>
                  <Drawer
                    anchor={anchor}
                    open={state[anchor]}
                    onClose={toggleDrawer(anchor, false)}
                  >
                    {list(anchor)}
                  </Drawer>
                </React.Fragment>
              ))}
          </Cell>
        </Grid>
        {rows}
      </Grid>
    </Root>
  );
}

export const TimeColumn = TimeColumnComponent;
