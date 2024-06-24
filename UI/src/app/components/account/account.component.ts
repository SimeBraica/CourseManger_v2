import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  user: any;

  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    // Subscribe to route parameters to handle GitHub authentication response
    this.route.queryParams.subscribe(params => {
      const code = params['code'];
      if (code) {
        // If the URL contains 'code' parameter (returned by GitHub), make a request to your API
        // to handle the authentication response
        this.http.get('https://localhost:7035/api/OAuth/account/github-response').subscribe(response => {
          this.user = response;
        });
      }
    });
  }
}
