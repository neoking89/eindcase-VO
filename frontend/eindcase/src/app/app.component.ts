import { Component } from '@angular/core';
import { CourseService } from './services/course.service';
import { CourseInstance } from './models/course-instance';
import { CourseTypeDetails } from './models/course';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  courseInstances: CourseInstance[] = [];

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    // use the courseService.getAll method to get all courses from SQLite in the backend endppoint.
    this.courseService
      .getAll('course-instances')
      .subscribe((courseInstances) => {
        for (let courseInstance of courseInstances as any) {
          for (let courseType of Object.values(CourseTypeDetails)) {
            if (courseInstance.courseId == courseType.id) {
              courseInstance.course = courseType;
              break;
            }
          }
        }
        this.courseInstances = courseInstances;
      });
  }
}
