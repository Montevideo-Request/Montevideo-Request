import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ParserService } from '../../../services/parser.service';
import { Guid } from 'guid-typescript';
import { Parser } from './../../../models/parser';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  loading = false;
  parser: Parser;
  parserForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';

  constructor(
    private parserService: ParserService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.parserForm = this.formBuilder.group({
      path: ['', Validators.compose([
        Validators.required, 
        Validators.pattern('^[a-zA-Z ]*$'),
        Validators.minLength(8),
      ])]
    });
  }

  get p() { return this.parserForm.controls; }

  
  public submit() {
    this.submitted = true;
    if (this.parserForm.invalid) {
      return;
    }
    this.parser.Id = Guid.create().toString();
    this.parserService.Convert(this.parser).subscribe(
      () => {
        //avisar que se envió
      },
      (error: any) => {
        this.errorMessage = error;
        this.error = true;
      }
    );
  }

  // onClosed(dismissedAlert: AlertComponent): void {
  onClosed(): void {
    this.error = false;
  }

}
