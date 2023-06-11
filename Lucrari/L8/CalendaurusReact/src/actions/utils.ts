import moment from "moment";

export function convertDayOfWeek(dayOfWeek: string): string {
  switch (dayOfWeek) {
    case "1":
      return "Monday";
    case "2":
      return "Tuesday";
    case "3":
      return "Wednesday";
    case "4":
      return "Thursday";
    case "5":
      return "Friday";
    default:
      return "";
  }
}

export const convertHourToTimespan = (hour: number) =>
{
  return `${hour > 9 ? hour : "0" + hour}:00:00`;
}

export const convertTimespanToHour = (timespan: string) =>
{
  return moment(timespan, "HH:mm:ss").hour();
}