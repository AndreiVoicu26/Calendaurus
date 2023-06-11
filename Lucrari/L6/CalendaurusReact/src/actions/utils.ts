export function isNullOrUndefined(data: any): boolean {
  return data === null || data === undefined;
}

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
