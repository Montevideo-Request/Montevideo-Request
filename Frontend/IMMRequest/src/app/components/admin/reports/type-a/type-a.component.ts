import { Component, OnInit } from '@angular/core';
import { ReportService } from './../../../../services/report.service';
import { Request } from './../../../../models/request';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { NgbDate, NgbCalendar, NgbPanelChangeEvent, NgbAccordion } from '@ng-bootstrap/ng-bootstrap';
import { DatepickerViewModel } from '@ng-bootstrap/ng-bootstrap/datepicker/datepicker-view-model';

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
  response: any = { keys: "", body: "" };
  hoveredDate: NgbDate | null = null;
  fromDate: NgbDate;
  toDate: NgbDate | null = null;
  typeForm: FormGroup;
  submitted = false;
  isGenerating = false;
  error = false;
  errorMessage = '';
  email: string;
  requests: Request[];
  groupedByState: {};

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

  get controls() {
    return this.typeForm.controls;
  }

  formatNGDate(date: NgbDate) {
    var month = date.month > 9 ? date.month : "0" + (date.month);
    var day = date.day > 9 ? date.day  : "0" + date.day;
    var responseDate = month + "/" + day + "/" + date.year

    return responseDate;
  }

  formatDate(date: Date) {
    var month = (date.getMonth() +1 )> 9 ? (date.getMonth() +1) : "0" + (date.getMonth() +1);
    var day = date.getDate() > 9 ? date.getDate()  : "0" + date.getDate();
    var responseDate = month + "/" + day + "/" + date.getFullYear();

    return responseDate;
  }

  parseDate(date: Date)
  {
      var dateToParse = new Date(date);
      return this.formatDate(dateToParse);
  }
 
  public generateReport() {
    this.isGenerating = true;
    if (this.typeForm.invalid) { return }

    this.reportService.generateReportA(this.email, this.formatNGDate(this.fromDate), this.formatNGDate(this.toDate))
    .subscribe((requests: Request[]) => {
        this.requests = requests;
        var groupedBy = {};

        this.requests.forEach(function (request) {
            groupedBy [request.state] = groupedBy [request.state] || [];
            groupedBy [request.state].push(request);
        });

        this.groupedByState = groupedBy;
        this.submitted = true;

    }, messageError => this.response.body = messageError);
  }
}
