import { Grid } from "@mui/material";
import styled from "styled-components";
import * as React from "react";

const Root = styled.div`
  display: block;
  background-color: white;
  color: black;
  width: fit-content;
  height: fit-content;
  border: 0.1rem solid gray;
`;

const Hour = styled.div`
  display: block;
  padding: 1rem;
  background-color: #f5f5f5;
`;

export function TimeColumnComponent() {
  const rows = [];
  for (let i = 8; i < 20; i++) {
    rows.push(
      <Grid
        item
        xs={8}
        key={i.toString()}
        sx={{ borderBottom: "0.1rem solid black", width: "6rem" }}
      >
        <Hour>{i.toString() + " - " + (i + 1).toString()}</Hour>
      </Grid>
    );
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
            width: "6rem",
          }}
        >
          <Hour>&nbsp;</Hour>
        </Grid>
        {rows}
      </Grid>
    </Root>
  );
}

export const TimeColumn = TimeColumnComponent;
