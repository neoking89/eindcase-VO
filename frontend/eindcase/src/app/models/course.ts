export interface ICourse {
  id: number;
  title: string;
  duration: number;
  code: string;
}

export enum CourseType {
  CSharp = 'CSharp',
  Java = 'Java',
}

// stuur naar endpoint courses
export const CourseTypeDetails = {
  [CourseType.CSharp]: {
    id: 1,
    code: 'CNETIN',
    title: 'C# Programmeren',
    duration: 5,
  },
  [CourseType.Java]: {
    id: 2,
    code: 'JPA',
    title: 'Java Programmeren',
    duration: 2,
  },
};

export class Course implements ICourse {
  id: number;
  title: string;
  duration: number;
  code: string;
  type: CourseType;

  constructor(courseType: CourseType) {
    const details = CourseTypeDetails[courseType];
    this.id = details.id;
    this.title = details.title;
    this.duration = details.duration;
    this.code = details.code;
    this.type = courseType;
  }
}
