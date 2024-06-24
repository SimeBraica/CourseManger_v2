import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AcademicYear } from '../../models/academicyear-model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AcademicyearServiceService {
  private restUrl = `https://${environment.restAPI}/AcademicYear`;

  constructor(private http: HttpClient) { }


  getAcademicYear(sort: string, filter: string, page:string, maxPageSize: string): Observable<AcademicYear[]> {
    const options = { withCredentials: true };
    return this.http.get<AcademicYear[]>(this.restUrl + "?sortBy=" + sort + "&is_active=" + filter + "&page=" + page + "&pageSize=" + maxPageSize, options);
  }

  DeleteAcademicYear(id: number): Observable<AcademicYear[]> {
    const url = `${this.restUrl}/${id}`;
    //const options = { withCredentials: true };
    return this.http.delete<AcademicYear[]>(url/*, options*/);
  }

  async createNewAcademicYear(newAcademicYear: { name: string; active: boolean; }) {
    const options = { withCredentials: true };
    const response = await this.http.post(this.restUrl, newAcademicYear, { observe: 'response', ...options }).toPromise();
    return response;
  }
}
