import { CourseType, CourseTypeDetails } from './course';

export function setDuration(startDate: Date, endDate: Date): number {
  const startDateobject = new Date(startDate);
  const endDateobject = new Date(endDate);
  const durationInMs = endDateobject.getTime() - startDateobject.getTime();
  const durationInDays = durationInMs / (1000 * 60 * 60 * 24);
  return Math.ceil(durationInDays);
}

export function validateDate(startDate: Date, endDate: Date): void {
  if (startDate.getDay() === 0 || startDate.getDay() === 6) {
    throw new Error('Selecteer een begindatum van maandag t/m vrijdag');
  }

  if (endDate.getDate() - startDate.getDate() > 5) {
    throw new Error('Kies een periode van maximaal 5 dagen');
  }
}

export function getCourseTypeFromCode(code: string): CourseType | null {
  for (const courseType of Object.values(CourseType)) {
    const details = CourseTypeDetails[courseType];
    if (details.code === code) {
      return courseType;
    }
  }
  return null;
}
