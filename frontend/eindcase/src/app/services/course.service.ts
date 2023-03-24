import { CourseInstance } from './../models/course-instance';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  apiUrl = environment.apiUrl;
  courses: CourseInstance[] = [];

  constructor(private http: HttpClient) {}

  public getById(id: number): Observable<CourseInstance> {
    return this.http.get<CourseInstance>(`${this.apiUrl}/${id}`);
  }

  public getAll(endpoint: string): Observable<CourseInstance[]> {
    const url = environment.apiUrl + endpoint;
    return this.http.get<CourseInstance[]>(url);
  }

  public add<T>(obj: T, endpoint: string): Observable<T> {
    const url = environment.apiUrl + endpoint;
    return this.http.post<T>(url, obj);
  }
}
