import { Component, OnInit } from '@angular/core';
import { ReportService } from './../../../../services/report.service';
import { Request } from './../../../../models/request';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-type-a',
  templateUrl: './type-a.component.html',
  styles: [`
    .custom-day {
      text-align: center;
      padding: 0.185rem 0.25rem;
      display: inline-block;
      height: 2rem;
      width: 2rem;
    }
    .custom-day.focused {
      background-color: #e6e6e6;
    }
    .custom-day.range, .custom-day:hover {
      background-color: rgb(2, 117, 216);
      color: white;
    }
    .custom-day.faded {
      background-color: rgba(2, 117, 216, 0.5);
    }
  `]
})
export class TypeAComponent implements OnInit {
  hoveredDate: NgbDate | null = null;
  fromDate: NgbDate;
  toDate: NgbDate | null = null;
  typeForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  email: string;
  constructor(private reportService: ReportService, calendar: NgbCalendar, private formBuilder: FormBuilder) {
    this.fromDate = calendar.getNext(calendar.getToday(), 'd', -10);
    this.toDate = calendar.getToday();
  }

  ngOnInit() {
    this.typeForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])]
    });
  }

  onDateSelection(date: NgbDate) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
    } else if (this.fromDate && !this.toDate && date.after(this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
  }

  isHovered(date: NgbDate) {
    return this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate);
  }

  isInside(date: NgbDate) {
    return this.toDate && date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return date.equals(this.fromDate) || (this.toDate && date.equals(this.toDate)) || this.isInside(date) || this.isHovered(date);
  }

  get a() {
    return this.typeForm.controls;
  }

  public generate() {
    this.submitted = true;
    if (this.typeForm.invalid) {
      return;
    }
    // ACA TENGO QUE HACER EL POST DEL REPORT
    // this.administrator.Id = Guid.create().toString();
    // console.log(this.administrator);
    // this.administratorService.add(this.administrator).subscribe(
    //   () => {
    //     this.bsModalRef.hide();
    //   },
    //   (error: any) => {
    //     this.errorMessage = error;
    //     this.error = true;
    //   }
    // );
  }
}
