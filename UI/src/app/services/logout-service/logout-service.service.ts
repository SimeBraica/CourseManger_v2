import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class LogoutServiceService {
  //private restUrl = `https://${environment.restAPI}/Teacher/logout`;
  private restUrl = `https://localhost:7035/api/Teacher/logout`;
  constructor(private http: HttpClient) { }

  async logoutTeacher(): Promise<HttpResponse<any>> {
    let response: any = null;
    const options = {
      withCredentials: true,
      observe: 'response' as const
    };

    try {
      response = await this.http.post<HttpResponse<any>>(this.restUrl, {}, options).toPromise();
      console.log(response);
      return response;
    } catch (error) {
      console.error('Logout failed', error);
      throw error;
    }
  }
}
