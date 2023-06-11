import { IPublicClientApplication } from "@azure/msal-browser";
import { prepareToken } from "../app/authUtils";
import * as constants from "../Constants";
import { Discipline, PracticalLesson } from "./models";

export async function loadDisciplines(
  instance: IPublicClientApplication,
  onLoaded: (disciplines: Discipline[]) => void
) {
  const token = await prepareToken(instance);
  const headers = new Headers();
  const bearer = "Bearer " + token;

  headers.append("Content-Type", "application/json");

  const options = {
    method: "GET",
    headers: headers,
  };

  headers.append("Authorization", bearer);

  fetch(constants.API_URL + "/Disciplines", options)
    .then((response) => {
      if (response.ok) {
        const disciplines: Promise<Discipline[]> = response.json();
        disciplines.then((d) => onLoaded(d));
      } else {
        console.log(response.statusText);
      }
    })
    .catch((err) => console.log(err));
}

export async function loadStudentDisciplines(
  instance: IPublicClientApplication,
  onLoaded: (studentDisciplines: Discipline[]) => void
) {
  const token = await prepareToken(instance);
  const headers = new Headers();
  const bearer = "Bearer " + token;

  headers.append("Content-Type", "application/json");

  const options = {
    method: "GET",
    headers: headers,
  };

  headers.append("Authorization", bearer);

  fetch(constants.API_URL + "/Students/disciplines", options)
    .then((response) => {
      if (response.ok) {
        const disciplines: Promise<Discipline[]> = response.json();
        disciplines.then((d) => onLoaded(d));
      } else {
        console.log(response.statusText);
      }
    })
    .catch((err) => console.log(err));
}

export async function loadStudentPractical(
  instance: IPublicClientApplication,
  onLoaded: (practicalLessons: PracticalLesson[]) => void
) {
  const token = await prepareToken(instance);
  const headers = new Headers();
  const bearer = "Bearer " + token;

  headers.append("Content-Type", "application/json");

  const options = {
    method: "GET",
    headers: headers,
  };

  headers.append("Authorization", bearer);

  fetch(constants.API_URL + "/Students/practical", options)
    .then((response) => {
      if (response.ok) {
        const practicalLessons: Promise<PracticalLesson[]> = response.json();
        practicalLessons.then((d) => onLoaded(d));
      } else {
        console.log(response.statusText);
      }
    })
    .catch((err) => console.log(err));
}
