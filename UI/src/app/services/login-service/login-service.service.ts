import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {
  private restUrl = `https://${environment.restAPI}/Teacher/login`; 

  constructor(private http: HttpClient) { }

  async loginTeacher(userCredentials: { username: string; password: string; }) {
      const options = { withCredentials: true };
      const response = await this.http.post(this.restUrl, userCredentials, { observe: 'response', ...options }).toPromise();
      return response;
    }
}
