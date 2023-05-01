import styled from "styled-components";
import { Button } from "./button";
import { useMsal } from "@azure/msal-react";
import { prepareToken } from "../app/authUtils";

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
const Table = styled.table`
  width: 100%;
`;

export interface AuthenticatedProps {
  onSignOut: () => void;
}

const Content = styled.div`
  overflow: auto;
  tr {
    height: 2rem;
    border-bottom: 1px solid mediumpurple;
  }
`;

export function Calendar(props: AuthenticatedProps) {
  const { onSignOut } = props;

  const { instance } = useMsal();
  const apiUrl = "";

  const getApiData = async () => {
    const token = await prepareToken(instance);
    const headers = new Headers();
    const bearer = "Bearer " + token;

    const options = {
      method: "GET",
      headers: headers,
    };

    headers.append("Authorization", bearer);

    const data = await fetch(apiUrl, options).then((d) => d.json());
  };

  return (
    <Root>
      <Header>
        Calendar
        <span></span>
        <Button onClick={onSignOut}>Log Out</Button>
      </Header>
      <Content>
        <Table>
          <thead>
            <th>Calendar</th>
          </thead>
          <tbody></tbody>
        </Table>
      </Content>
    </Root>
  );
}
