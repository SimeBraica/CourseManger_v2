import { AcademicyearServiceService } from '../../services/academic-year-service/academicyear-service.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AcademicYear } from '../../models/academicyear-model';

@Component({
  selector: 'app-academicyear-component',
  templateUrl: './academicyear-component.component.html',
  styleUrl: './academicyear-component.component.css'
})
export class AcademicyearComponentComponent {
  academicYears: AcademicYear[] = [];
  showAddPopup: boolean = false;
  name: string = '';
  active: boolean = false;
  currentPage = 1;
  isSorted = true;
  isActive = true;
  maxResultsPerPage = 100;
  totalPages = 1;
  ngOnInit() {
    this.LoadAcademicYears();
  }
  constructor(
    private academicYearService: AcademicyearServiceService,
    private router: Router
  ) { }



  LoadAcademicYears() {
    const sortOrder = this.isSorted ? 'desc' : 'asc';
    const filterActive = this.isActive ? 'true' : 'false';
    this.academicYearService.getAcademicYear(sortOrder, filterActive, this.currentPage.toString(), this.maxResultsPerPage.toString()).subscribe(
      (response: any) => {
        this.academicYears = response.data;
        this.totalPages = response.totalPages;
        this.currentPage = response.currentPage;
      },
    );
  }


  ShowAcademicYearsDetails(id: number) {
    this.router.navigate([`/academicYears/${id}`]);
  }

  RemoveAcademicYear(id: number) {
    this.academicYearService.DeleteAcademicYear(id).subscribe(
      () => {
        this.academicYears = this.academicYears.filter(year => year.id !== id);
      });
    //this.router.navigate(['/academicYears'])
  }

  toggleAddPopup() {
    this.showAddPopup = !this.showAddPopup;
  }

  async addAcademicYear() {
    const newAcademicYear = {
      name: this.name,
      active: this.active
    };
    try {
      let response = await this.academicYearService.createNewAcademicYear(newAcademicYear);
      if (response) {
        console.log("Added new course");
        window.location.reload();
      }
    } catch (error) {
      console.error("Error during creation", error);
    }
    this.showAddPopup = false;
  }
}
