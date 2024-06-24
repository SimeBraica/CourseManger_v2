import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegistrationServiceService {
  private restUrl = `https://${environment.restAPI}/Teacher/registration`;

  constructor(private http: HttpClient) { }

  async registerTeacher(userCredentials: { username: string; password: string; firstName: string; lastName: string }) {
    const response = await this.http.post(this.restUrl, userCredentials, { observe: 'response' }).toPromise();
    return response;
  }
}
