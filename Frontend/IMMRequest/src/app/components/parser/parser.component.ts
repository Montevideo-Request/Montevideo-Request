import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ParserService } from '../../services/parser.service';
import { Guid } from 'guid-typescript';
import { Parser } from './../../models/parser';
import { Format } from './../../models/format';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { StringDecoder } from 'string_decoder';

@Component({
  selector: 'app-parser',
  templateUrl: './parser.component.html',
  styleUrls: ['./parser.component.css']
})
export class ParserComponent implements OnInit {
  response: any = { keys: "", body: "" };
  selectedFormat: boolean;
  isSubmitted: boolean;
  fields: [];
  formats: [];
  format: string;
  FilePath: string;
  parser: Parser;
  parserForm: FormGroup;
  loading = false;
  error = false;
  errorMessage = '';
  success = false;
  successMessage = 'The Parsing upload was successful.'
  faSpinnner = faSpinner;

  constructor(
    private parserService: ParserService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.parserForm = this.formBuilder.group({
      Type: ['', [Validators.required]]
    });

    this.parserService
      .GetFormats()
      .subscribe((formats: []) => this.formats = formats, messageError => this.response.body = messageError);

  }

  public formatSelected(format: string) {
    this.parserForm.controls['Type'].setValue(format['value']);
    this.parserService.GetFields(format['value']).subscribe((fields: []) => {
        this.fields = fields;
        for (const key in this.fields) {
            if (this.fields.hasOwnProperty(key)) {
                const control: FormControl = new FormControl(this.fields[key], Validators.required);
                this.parserForm.addControl(this.fields[key], control);
            }
        }
    }, messageError => this.response.body = messageError);

    this.selectedFormat = true;
  }

  public submit() {
    this.isSubmitted = true;
    this.parser = this.parserForm.value;

    if (this.parserForm.invalid) {
      return;
    }
    this.parserService.Convert(this.parser).subscribe(() => {
        this.success = true;
        this.error = false;
      },
      (error: any) => {
        this.errorMessage = error;
        this.error = true;
        this.success = false;
      }
    );
  }

  onClosed(): void {
    this.error = false;
  }
}
