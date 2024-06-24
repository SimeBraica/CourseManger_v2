import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Course } from '../../models/course-model';
import { StudyProgramWithId } from '../../models/studyprogramwithid-model';

@Injectable({
  providedIn: 'root'
})
export class AcademicyearDetailsServiceService {

  private baseUrl = `https://${environment.restAPI}/CourseInAcademicYear/CourseYears`;
  private restUrl = `https://${environment.restAPI}/StudyProgram/StudyProgramWithId`;
  constructor(private http: HttpClient) { }

  GetAcademicYearCourses(id: number): Observable<Course[]> {
    const options = { withCredentials: true };
    const url = `${this.baseUrl}/${id}`;
    return this.http.get<Course[]>(url, options);

  }

  getStudyProgramsWithId(): Observable<StudyProgramWithId[]> {
    const options = { withCredentials: true };
    return this.http.get<StudyProgramWithId[]>(this.restUrl, options);
  }

  async createNewCourse(newCourse: { name: string; isvuId: string; ects: number; semester: number; studyProgramId: number }, id: number) {
    const options = { withCredentials: true };
    const url = `${this.baseUrl}/${id}`;
    const response = await this.http.post(url, newCourse, { observe: 'response', ...options }).toPromise();
    return response;
  }
}
