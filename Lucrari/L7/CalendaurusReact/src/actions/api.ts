import { IPublicClientApplication } from "@azure/msal-browser";
import { prepareToken } from "../app/authUtils";
import * as constants from "../Constants";
import {
  Discipline,
  PracticalLesson,
  PracticalLessonEventEnrollment,
} from "./models";


/*refactor the api's to use a customFetch method

customFetch contains the data that was commonly used in all api's
customFetch requires specific data: patch, RequestInit, instance and onLoaded
*/
const customFetch = async <TData>(
  path: string,
  init: RequestInit,
  instance: IPublicClientApplication,
  onLoaded: (data: TData) => void
) => {
  const token = await prepareToken(instance);
  const headers = new Headers();
  const bearer = "Bearer " + token;

  headers.append("Content-Type", "application/json");

  const options: RequestInit = {
    ...init,
    headers: headers,
  };

  headers.append("Authorization", bearer);

  fetch(constants.API_URL + path, options)
    .then((response) => {
      if (response.ok) {
        if (options.method !== "GET") {
          onLoaded(undefined as never);
        } else {
          const data: Promise<TData> = response.json();
          data.then((d) => onLoaded(d));
        }
      } else {
        console.log(response.statusText);
      }
    })
    .catch((err) => console.log(err));
};

export const loadDisciplines = async (
  instance: IPublicClientApplication,
  onLoaded: (disciplines: Discipline[]) => void
) => customFetch("/Disciplines", { method: "GET" }, instance, onLoaded);

export const loadStudentDisciplines = async (
  instance: IPublicClientApplication,
  onLoaded: (studentDisciplines: Discipline[]) => void
) =>
  customFetch("/Students/disciplines", { method: "GET" }, instance, onLoaded);

export const loadStudentEnrollments = async (
  instance: IPublicClientApplication,
  onLoaded: (enrollments: PracticalLesson[]) => void
) =>
  customFetch("/Students/enrollments", { method: "GET" }, instance, onLoaded);

export const loadStudentPractical = async (
  instance: IPublicClientApplication,
  onLoaded: (practicalLessons: PracticalLesson[]) => void
) => customFetch("/Students/practical", { method: "GET" }, instance, onLoaded);

export const enrollStudent = async (
  instance: IPublicClientApplication,
  onLoaded: (practicalLessonEvent: PracticalLessonEventEnrollment) => void,
  body: PracticalLessonEventEnrollment
) =>
  customFetch(
    "/Students/enrollments",
    { method: "POST", body: JSON.stringify(body) },
    instance,
    onLoaded
  );

export const unenrollStudent = async (
  instance: IPublicClientApplication,
  onLoaded: (practicalLessonEvent: PracticalLessonEventEnrollment) => void,
  practicalLessonId: number
) =>
  customFetch(
    `/Students/enrollments/${practicalLessonId}`,
    { method: "DELETE" },
    instance,
    onLoaded
  );
