import { Course, CourseType } from './course';

interface ICourseInstance {
  startDate: Date;
  course: Course;
}

export class CourseInstance implements ICourseInstance {
  startDate = new Date();
  course: Course;

  constructor(
    title = CourseType.CSharp,
    startDate: Date = new Date()
  ) {
    this.course = new Course(title);
    this.startDate = startDate;
  }
}
