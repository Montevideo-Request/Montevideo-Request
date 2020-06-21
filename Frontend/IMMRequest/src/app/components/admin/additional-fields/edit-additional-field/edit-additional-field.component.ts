import { AdditionalField } from './../../../../models/additionalField';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdditionalFieldService } from '../../../../services/additional-field.service';

@Component({
  selector: 'app-edit-additional-field',
  templateUrl: './edit-additional-field.component.html',
  styleUrls: ['./edit-additional-field.component.css']
})
export class EditAdditionalFieldComponent implements OnInit {
  additionalField: AdditionalField;
  closeBtnName: string;
  editForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(public bsModalRef: BsModalRef, private additionalFieldService: AdditionalFieldService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required])]
    });
  }

  get a() { return this.editForm.controls; }

  public submit() {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }
    this.additionalFieldService.edit(this.additionalField).subscribe(
      () => {
        this.bsModalRef.hide();
      },
      ((error: any) => {
        this.errorMessage = error;
        this.error = true;
      })
    );
  }

  onClosed(): void {
    // onClosed(dismissedAlert: AlertComponent): void {
    this.error = false;
  }

}
