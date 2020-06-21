import { AdditionalField } from './../../../../models/additionalField';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdditionalFieldService } from '../../../../services/additional-field.service';
import { Guid } from "guid-typescript";

@Component({
  selector: 'app-add-additional-field',
  templateUrl: './add-additional-field.component.html',
  styleUrls: ['./add-additional-field.component.css']
})
export class AddAdditionalFieldComponent implements OnInit {
  closeBtnName: string;
  registerForm: FormGroup;
  additionalFields: AdditionalField[];
  additionalField: AdditionalField = new AdditionalField('', '', '', '', false, null);
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(
    public bsModalRef: BsModalRef,
    private additionalFieldService: AdditionalFieldService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^[a-zA-Z ]*$')
        ])
      ]
    });
  }

  get a() {
    return this.registerForm.controls;
  }

  public submit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.additionalField.id = Guid.create().toString();
    this.additionalFieldService.addAdditionalField(this.additionalField).subscribe(
      () => {
        this.bsModalRef.hide();
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