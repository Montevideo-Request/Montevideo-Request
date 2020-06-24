
import { Component, OnInit } from '@angular/core';
import { ReportService } from './../../../../services/report.service';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
@Component({
    selector: 'app-type-b',
    templateUrl: './type-b.component.html',
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
export class TypeBComponent implements OnInit {
    response: any = { keys: "", body: "" };
    hoveredDate: NgbDate | null = null;
    fromDate: NgbDate;
    toDate: NgbDate | null = null;
    typeForm: FormGroup;
    submitted = false;
    error = false;
    errorMessage = '';
    email: string;
    reportElements: [];

    constructor(private reportService: ReportService, calendar: NgbCalendar, private formBuilder: FormBuilder) {
        this.fromDate = calendar.getNext(calendar.getToday(), 'd', -10);
        this.toDate = calendar.getToday();
    }

    ngOnInit() {
        this.typeForm = this.formBuilder.group({});
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

    get controls() {
        return this.typeForm.controls;
    }

    formatNGDate(date: NgbDate) {
        var month = date.month > 9 ? date.month : "0" + (date.month);
        var day = date.day > 9 ? date.day : "0" + date.day;
        var responseDate = month + "/" + day + "/" + date.year

        return responseDate;
    }


    public generate() {
        if (this.typeForm.invalid || !this.toDate || !this.fromDate) { return }
        this.reportService.generateReportB(this.formatNGDate(this.fromDate), this.formatNGDate(this.toDate))
            .subscribe((response: []) => {
                this.reportElements = response;
                this.submitted = true;

                this.reportElements.sort(function (a, b) {
                    /* Sory By Occurences */
                    if (a["quantity"] < b["quantity"]) return 1;
                    if (a["quantity"] > b["quantity"]) return -1;
                
                    /* Sort By Type Creation Date */
                    if (a["type"]["Date"] < b["type"]["Date"]) return 1;
                    if (a["type"]["Date"] > b["type"]["Date"]) return -1;
                });
            }, messageError => this.response.body = messageError);
    }
}
