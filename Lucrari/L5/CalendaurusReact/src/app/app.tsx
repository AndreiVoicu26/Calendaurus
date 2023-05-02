import styled from "styled-components";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { NotAuthenticated } from "../components/notAuthenticated";
import { Calendar } from "../components/calendar";
import { loginRequest } from "../authConfig";

const Root = styled.div`
  display: grid;
  place-items: center;
  position: absolute;
  inset: 0;
`;

function useAuth() {
  const { instance } = useMsal();
  const isAuth = useIsAuthenticated();

  const signIn = () => {
    instance
      .loginPopup(loginRequest)

      .catch((e) => {
        console.log(e);
      });
  };
  const signOut = () => {
    instance.logoutPopup({
      postLogoutRedirectUri: "/",
      mainWindowRedirectUri: "/",
    });
  };

  return {
    isAuth,
    signIn,
    signOut,
  };
}

function App() {
  const { signOut, signIn, isAuth } = useAuth();
  return (
    <Root>
      {!isAuth && <NotAuthenticated onSignIn={signIn} />}
      {isAuth && <Calendar onSignOut={signOut} />}
    </Root>
  );
}

export default App;
