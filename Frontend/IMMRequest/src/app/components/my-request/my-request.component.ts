import { Component, OnInit } from '@angular/core';
import { Request } from './../../models/request';
import { RequestService } from '../../services/request.service';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';

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

  requestorsName = "name";
  requestorsEmail = "email";
  

  constructor(
    private requestService: RequestService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.requestForm = this.formBuilder.group({
      givenId: ['', Validators.required]
    });

    this.requestService.getById(this.givenId).subscribe(
      () => {

      },
      (error: any) => {
        this.hasError = true;
        this.error = error;
      }
    );
  }

  get r() { return this.requestForm.controls; }

  getRequest() {
    console.log(this.r.givenId.value);
    this.requestService.getById(this.r.givenId.value).subscribe((request: Request) => this.request = request, messageError => this.response.body = messageError);
  }

}
