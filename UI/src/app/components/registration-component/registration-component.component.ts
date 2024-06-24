import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { RegistrationServiceService } from '../../services/registration-service/registration-service.service';

@Component({
  selector: 'app-registration-component',
  templateUrl: './registration-component.component.html',
  styleUrl: './registration-component.component.css'
})
export class RegistrationComponentComponent {
  username: string = '';
  password: string = '';
  lastName: string = '';
  firstName: string = '';

  constructor(
    private http: HttpClient,
    private registrationService: RegistrationServiceService,
    private router: Router
  ) { }

  async registration() {
    const formLogin = {
      username: this.username,
      password: this.password,
      firstName: this.firstName,
      lastName: this.lastName
    };

    try {
      let response = await this.registrationService.registerTeacher(formLogin);
      if (response) {
        console.log("Registration successful");
        this.router.navigate(['/login']);
      }
    } catch (error) {
      console.error("Error during registration", error);
    }
  }
}
