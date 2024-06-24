
import { LogoutServiceService } from '../../services/logout-service/logout-service.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout-component',
  templateUrl: './logout-component.component.html',
  styleUrls: ['./logout-component.component.css'] 
})

export class LogoutComponentComponent implements OnInit {

  constructor(
    private logoutService: LogoutServiceService,
    private router: Router
  ) { }

  async ngOnInit(): Promise<void> {
    try {
      let response = await this.logoutService.logoutTeacher();
      if (response) {
        console.log("Logout successful");
        this.router.navigate(['/login']);
      }
    } catch (error) {
      console.error("Error during logout", error);
    }
  }
}
