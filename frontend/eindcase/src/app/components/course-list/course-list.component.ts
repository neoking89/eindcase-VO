import { CourseService } from './../../services/course.service';
import { CourseInstance } from './../../models/course-instance';
import { Course, CourseType, CourseTypeDetails } from './../../models/course';
import { AppComponent } from './../../app.component';
import {
  Component,
  EventEmitter,
  Output,
  Input,
  Injectable,
  ViewChild,
  ElementRef,
} from '@angular/core';
import {
  getCourseTypeFromCode,
  validateDate,
} from 'src/app/models/helper-functions';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css'],
})
@Injectable()
export class CourseListComponent {
  courseTypes = Object.values(CourseType);
  @Output() courseInstancesUpdated = new EventEmitter<CourseInstance[]>();
  @Input() courseInstances: CourseInstance[] = [];
  @ViewChild('fileInput') fileInput?: ElementRef<HTMLInputElement>;

  startDateString: string = '';
  endDateString: string = '';
  errorMessage: string = '';
  selectedCourseType: CourseType = CourseType.CSharp;
  courseInstance: CourseInstance = new CourseInstance();

  constructor(
    private courseService: CourseService,
    private appComponent: AppComponent
  ) {}

  loopThroughLines(text: string) {
    const lines = text.split('\r\n');
    let courseType: CourseType | null = null;
    let startDate: string | null = null;
    let attr: string = '';
    for (const line of lines) {
      if (!line.includes(':')) {
        continue;
      }
      try {
        attr = line.split(':')[1].trim();
      } catch (e: any) {
        console.log(`Error while reading line: ${line}. ${e.message}`);
      }
      if (line.toLowerCase().startsWith('cursuscode')) {
        courseType = getCourseTypeFromCode(attr);
      } else if (line.toLowerCase().startsWith('startdatum')) {
        startDate = attr;
      }
      if (courseType && startDate) {
        this.addCourse(startDate, courseType);
        courseType = null;
        startDate = null;
      }
    }
  }

  readText() {
    const fileInput = this.fileInput!.nativeElement;
    if (fileInput.files && fileInput.files[0]) {
      const file = fileInput.files[0];
      const reader = new FileReader();
      reader.onload = (e) => {
        const contents = reader.result;
        this.loopThroughLines(contents as string);
      };
      reader.readAsText(file);
    }
  }

  addCourse(startDate: string, courseType?: CourseType) {
    const startDateObj = new Date(startDate);
    if (courseType) {
      this.selectedCourseType = courseType;
    }
    const course = new Course(this.selectedCourseType);
    const endDate = new Date(startDate);
    endDate.setDate(startDateObj.getDate() + course.duration);
    try {
      validateDate(startDateObj, endDate);
    } catch (error: any) {
      this.errorMessage = error.message;
      return;
    }
    const courseInstance = new CourseInstance(course.type, startDateObj);

    const courseInstanceObj = {
      startDate: courseInstance.startDate,
      courseId: courseInstance.course.id,
    };
    const courseObj = {
      title: course.title,
      duration: course.duration,
      code: course.code,
    };

    this.addCourseToServer(courseObj);
    this.addCourseInstanceToServer(courseInstanceObj);
    this.appComponent.ngOnInit();
  }

  private addCourseInstanceToServer(courseInstanceObj: {
    startDate: Date;
    courseId: number;
  }) {
    this.courseService.add(courseInstanceObj, 'course-instances').subscribe({
      next: (courseInstance: any) => {
        console.log('Course instance added:', courseInstance);
      },
      error: (error: any) => {
        console.error('Error adding course instance:', error);
      },
    });
  }

  private addCourseToServer(courseObj: {
    title: string;
    duration: number;
    code: string;
  }) {
    this.courseService.add(courseObj, 'courses').subscribe((course: any) => {
      console.log('Course added:', course);
    });
  }
}
