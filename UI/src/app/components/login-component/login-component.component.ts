
import { LoginServiceService } from '../../services/login-service/login-service.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login-component.component.html',
  styleUrls: ['./login-component.component.css']
})
export class LoginComponentComponent {
  username: string = '';
  password: string = '';

  constructor(
    private loginService: LoginServiceService,
    private router: Router
  ) { }

  async login() {
    const formLogin = {
      username: this.username, 
      password: this.password
    };

    try {
      let response = await this.loginService.loginTeacher(formLogin); 
      if (response) {
        console.log("Login successful");
        this.router.navigate(['/academicYears']);
      }
    } catch (error) {
      console.error("Error during login", error);
    }
  }


}
