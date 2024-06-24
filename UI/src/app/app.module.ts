import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LoginComponentComponent } from '../app/components/login-component/login-component.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationComponentComponent } from '../app/components/registration-component/registration-component.component';
import { LogoutComponentComponent } from '../app/components/logout-component/logout-component.component';
import { AcademicyearComponentComponent } from '../app/components/academicyear-component/academicyear-component.component';
import { AcademicyearDetailsComponentComponent } from '../app/components/academicyear-details-component/academicyear-details-component.component';
import { StudyprogramComponentComponent } from './components/studyprogram-component/studyprogram-component.component';
import { AccountComponent } from './components/account/account.component';
const routes: Routes = [
  { path: 'login', component: LoginComponentComponent },
  { path: 'registration', component: RegistrationComponentComponent },
  { path: 'logout', component: LogoutComponentComponent },
  { path: 'academicYears', component: AcademicyearComponentComponent },
  { path: 'academicYears/:id', component: AcademicyearDetailsComponentComponent },
  { path: 'studyPrograms', component: StudyprogramComponentComponent },
  { path: 'account/github-response', component: AccountComponent },
];
// adawd
// fasfafaw

@NgModule({
  declarations: [
    AppComponent,
    LoginComponentComponent,
    RegistrationComponentComponent,
    LogoutComponentComponent,
    AcademicyearComponentComponent,
    AcademicyearDetailsComponentComponent,
    StudyprogramComponentComponent,
    AccountComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
