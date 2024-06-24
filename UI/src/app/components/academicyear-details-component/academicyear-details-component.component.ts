import { Component, OnInit } from '@angular/core';
import { Course } from '../../models/course-model';
import { AcademicyearDetailsServiceService } from '../../services/academic-year-details-service/academicyear-details-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudyProgramWithId } from '../../models/studyprogramwithid-model';

@Component({
  selector: 'app-academicyear-details-component',
  templateUrl: './academicyear-details-component.component.html',
  styleUrls: ['./academicyear-details-component.component.css']
})
export class AcademicyearDetailsComponentComponent implements OnInit {
  courses: Course[] = [];
  id: number | any;
  showAddPopup: boolean = false;
  name: string = '';
  shortName: string = '';
  isvuId: string = '';
  ects: number = 0;
  semester: number = 0;
  studyPrograms: StudyProgramWithId[] = [];
  studyProgramId: number = 0;

  constructor(
    private academicYearDetailsService: AcademicyearDetailsServiceService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.LoadCourses();
      this.loadStudyPrograms();
    });
  }

  LoadCourses() {
    this.academicYearDetailsService.GetAcademicYearCourses(this.id).subscribe(
      (data: Course[]) => {
        this.courses = data;
      }
    );
  }

  loadStudyPrograms() {
    this.academicYearDetailsService.getStudyProgramsWithId().subscribe(
      (data: StudyProgramWithId[]) => {
        this.studyPrograms = data;
      }
    );
  }

  toggleAddPopup() {
    this.showAddPopup = !this.showAddPopup;
  }

  async addCourseToAcademicYear(id: number) {
    const newCourseInAcademicYear = {
      name: this.name,
      isvuId: this.isvuId,
      ects: this.ects,
      semester: this.semester,
      studyProgramId: this.studyProgramId,
    };
    try {
      let response = await this.academicYearDetailsService.createNewCourse(newCourseInAcademicYear, id);
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
