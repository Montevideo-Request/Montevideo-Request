import { Component, OnInit, Directive, ElementRef, ViewChild } from '@angular/core';
import { Request } from './../../models/request';
import { RequestService } from '../../services/request.service';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import {NgModule} from '@angular/core';


@Component({
  selector: 'app-my-request',
  templateUrl: './my-request.component.html',
  styleUrls: ['./my-request.component.css']
})

export class MyRequestComponent implements OnInit {
  requestForm: FormGroup;
  givenId: number;
  response: any = { keys: "", body: "" };
  request: Request;
  x: number;
  success = false;
  hasError = false;
  error = '';
  dayDifference: string;

  requestorsName = "name";
  requestorsEmail = "email";

  @ViewChild('name') child: HTMLElement;

  ngAfterViewInit() {
    console.log("ngAfterViewInit", this.child);
  }

  constructor(
    private requestService: RequestService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.requestForm = this.formBuilder.group({
      givenId: ['', Validators.required]
    });
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

  calculateDiff(sentDate) {
    var date1:any = new Date(sentDate);
    var date2:any = new Date();
    var diffDays:any = Math.floor((date2 - date1) / (1000 * 60 * 60 * 24));

    return diffDays;
}


  getRequest() {
    if (this.requestForm.invalid) { return }
    var requestToSearch = this.requestForm.value["givenId"];

    this.requestService.getById(requestToSearch).subscribe((request: Request) => {
        this.request = request;
        var diff = this.calculateDiff(this.request.date);

        this.dayDifference = diff > 0 ? (diff + ' Days Ago') : 'Today';

    }, messageError => this.response.body = messageError);
  }

}
