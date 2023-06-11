import styled from "styled-components";
import { Button } from "./button";

const Root = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 1rem;
`;
const Title = styled.div`
  font-size: 3rem;
`;

export interface NotAuthenticatedProps {
  onSignIn: () => void;
}

export function NotAuthenticated(props: NotAuthenticatedProps) {
  const { onSignIn } = props;
  return (
    <Root>
      <Title>You are not authenticated</Title>
      <Button onClick={onSignIn}> Sign In </Button>
    </Root>
  );
}
