import { Component, OnInit } from '@angular/core';
import { StudyProgram } from '../../models/studyprogram-model';
import { StudyprogramServiceService } from '../../services/studyprogram-service/studyprogram-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-studyprogram-component',
  templateUrl: './studyprogram-component.component.html',
  styleUrls: ['./studyprogram-component.component.css']
})
export class StudyprogramComponentComponent implements OnInit {
  studyPrograms: StudyProgram[] = [];
  showAddPopup: boolean = false;
  showUpdatePopup: boolean = false;
  shortName: string = '';
  name: string = '';
  currentStudyProgramId: number | null = null;

  constructor(
    private studyProgramService: StudyprogramServiceService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadStudyPrograms();
  }

  loadStudyPrograms() {
    this.studyProgramService.getStudyPrograms().subscribe(
      (data: StudyProgram[]) => {
        this.studyPrograms = data;
      },
      error => {
        console.error("Error fetching study programs", error);
      }
    );
  }

  toggleAddPopup() {
    this.showAddPopup = !this.showAddPopup;
  }

  toggleUpdatePopup() {
    this.showUpdatePopup = !this.showUpdatePopup;
  }

  populateAndToggleUpdatePopup(studyProgram: StudyProgram) {
    this.shortName = studyProgram.shortName;
    this.name = studyProgram.name;
    this.toggleUpdatePopup();
  }

  async addStudyProgram() {
    const newStudyProgram = {
      shortName: this.shortName,
      name: this.name
    };
    try {
      const response = await this.studyProgramService.createStudyProgram(newStudyProgram);
      if (response) {
        console.log("Added new study program");
        this.loadStudyPrograms();
      }
    } catch (error) {
      console.error("Error during creation", error);
    }
    this.showAddPopup = false;
  }

  async updateStudyProgram() {
    const updatedStudyProgram = {
      shortName: this.shortName,
      name: this.name
    };

    try {
      const response = await this.studyProgramService.updateStudyProgram(updatedStudyProgram);
      if (response) {
        console.log("Updated study program");
        this.loadStudyPrograms();
      }
    } catch (error) {
      console.error("Error during update", error);
    }
    
    console.log("Update");
    this.showUpdatePopup = false;


  }

  async deleteStudyProgram(id: number) {
    try {
      const response = await this.studyProgramService.deleteStudyProgramById(id);
      this.loadStudyPrograms();
    } catch (error) {
      console.error('Error deleting study program:', error);
    }
  }

}
