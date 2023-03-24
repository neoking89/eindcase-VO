import { AppComponent } from './../../app.component';
import { FormsModule } from '@angular/forms';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CourseListComponent } from './course-list.component';
import { Course } from 'src/app/models/course';
import { CourseService } from 'src/app/services/course.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CourseListComponent', () => {
  let component: CourseListComponent;
  let fixture: ComponentFixture<CourseListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CourseListComponent, AppComponent],
      imports: [FormsModule, HttpClientTestingModule],
      providers: [CourseService, AppComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should call course service with correct parameters for adding course instance', () => {
    const courseInstanceObj = {
      startDate: new Date('2022-01-01'),
      courseId: 1,
    };
    const courseService = TestBed.inject(CourseService);
    spyOn(courseService, 'add');
    component['addCourseInstanceToServer'](courseInstanceObj);
    expect(courseService.add).toHaveBeenCalledWith(
      courseInstanceObj,
      'course-instances'
    );
  });

//   it('should add courses when loopThroughLines is called', () => {
//     const text: string = `Titel: C# Programmeren

//     Cursuscode: CNETIN
    
//     Duur: 5 dagen
    
//     Startdatum: 8/10/2018
//     `;
//     spyOn(component, 'addCourse');
//     component.loopThroughLines(text);
//     expect(component.addCourse).toHaveBeenCalledTimes(1);
//   });
});
