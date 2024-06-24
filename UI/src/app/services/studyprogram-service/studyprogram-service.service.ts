import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StudyProgram } from '../../models/studyprogram-model';

@Injectable({
  providedIn: 'root'
})
export class StudyprogramServiceService {
  private restUrl = `https://${environment.restAPI}/StudyProgram`;

  constructor(private http: HttpClient) { }


  getStudyPrograms(): Observable<StudyProgram[]> {
    const options = { withCredentials: true };
    return this.http.get<StudyProgram[]>(this.restUrl, options);
  }


  async createStudyProgram(newStudyProgram: { shortName: string; name: string; }) {
    const options = { withCredentials: true };
    const response = await this.http.post(this.restUrl, newStudyProgram, { observe: 'response', ...options }).toPromise();
    return response;
  }

  async updateStudyProgram(updateStudyProgram: { shortName: string; name: string; }) {
    const options = { withCredentials: true };
    const response = await this.http.put(this.restUrl, updateStudyProgram, { observe: 'response', ...options }).toPromise();
    return response;
  }

  async getStudyProgramById(id: number) {
    const options = { withCredentials: true };
   return this.http.get<StudyProgram[]>(this.restUrl+"/"+id, options);

  }

  async deleteStudyProgramById(id: number): Promise<any> {
    const options = { withCredentials: true };
    const url = `${this.restUrl}/${id}`;
    try {
      const response = await this.http.delete(url, options).toPromise();
      return response;
    } catch (error) {
      throw error;
    }
  }
}

