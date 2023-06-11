import { IPublicClientApplication, SilentRequest } from "@azure/msal-browser";

export async function prepareToken(instance: IPublicClientApplication) {
  const account = instance.getAllAccounts()[0];
  const accessTokenRequest: SilentRequest = {
    scopes: ["user.read"],
    account: account,
  };
  const tokenResponse = await instance.acquireTokenSilent(accessTokenRequest);
  console.log(tokenResponse.idToken);
  return tokenResponse.idToken;
}
