import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Guid } from "guid-typescript";

import { AdditionalField } from './../../../../models/additionalField';
import { Type } from './../../../../models/type';

import { AdditionalFieldService } from '../../../../services/additional-field.service';
import { TypeService } from '../../../../services/type.service';
import { FieldRange } from 'src/app/models/fieldRange';

@Component({
  selector: 'app-add-additional-field',
  templateUrl: './add-additional-field.component.html',
  styleUrls: ['./add-additional-field.component.css']
})
export class AddAdditionalFieldComponent implements OnInit {
  response: any = { keys: "", body: "" };
  closeBtnName: string;
  additionalFieldForm: FormGroup;
  additionalFields: AdditionalField[];
  additionalField: AdditionalField = new AdditionalField('', '', '', '', false, null);
  submitted = false;
  checked = false;
  isPositive = false;
  types: Type[];
  type: Type;
  selectedFieldType = '';
  selectedType = '';
  error = false;
  errorMessage = '';
  singleRange = '';
  fieldRangesText = '';
  singleFieldRange: FieldRange = new FieldRange('', '', '');
  constructor(
    public bsModalRef: BsModalRef,
    private additionalFieldService: AdditionalFieldService,
    private typeService: TypeService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.additionalFieldForm = this.formBuilder.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^[a-zA-Z ]*$')
        ])
      ]
    });

    this.typeService
      .getTypes()
      .subscribe((types: Type[]) => this.types = types, messageError => this.response.body = messageError);
  }

  public multiSelectSelected() {
    this.checked = !this.checked;
  }

  public changePositive() {
    this.isPositive = !this.isPositive;
  }

  get a() {
    return this.additionalFieldForm.controls;
  }

  public submit() {
    this.submitted = true;
    if (this.additionalFieldForm.invalid) {
      return;
    }
    /* desde */
    this.additionalField.id = Guid.create().toString();
    this.types.forEach(function(data) {
      if (data.name === this.selectedType) {
        this.additionalField.typeId = data.id;
      }
    }.bind(this));
    this.additionalField.multiSelect = this.checked;

    // if (this.checked) {
    //     const splitedRanges = this.fieldRangesText.split(/\r?\n/);
    //     console.log(splitedRanges);
    // } else {
    //   if (this.selectedFieldType == "Bool") {
    //     // 
    //   } else {
    //     this.singleFieldRange.id = Guid.create().toString();
    //     this.singleFieldRange.additionalFieldId = this.additionalField.id;
    //     this.singleFieldRange.range = "test"; // bindear con singleField en el html
    //     console.log(this.singleFieldRange);
    //     //this.additionalField.ranges.push(this.singleFieldRange);
    //     console.log(this.additionalField);
    //   }
    // }
    /* hasta */
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