import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ParserService } from '../../services/parser.service';
import { Guid } from 'guid-typescript';
import { Parser } from './../../models/parser';
import { Format } from './../../models/format';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-parser',
  templateUrl: './parser.component.html',
  styleUrls: ['./parser.component.css']
})
export class ParserComponent implements OnInit {
  selectedFormat = false;
  response: any = { keys: "", body: "" };
  formats: Format[];
  format: Format;
  parser: Parser;
  parserForm: FormGroup;
  options: string[];
  loading = false;
  submitted = false;
  error = false;
  errorMessage = '';
  faSpinnner = faSpinner;

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

    this.parserService
      .GetFormats()
      .subscribe((formats: Format[]) => this.formats = formats, messageError => this.response.body = messageError);
  }

  get p() { return this.parserForm.controls; }

  public formatSelected() {
    this.selectedFormat = true;
    this.options = this.format.Options;
  }

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
