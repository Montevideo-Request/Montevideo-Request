import { Type } from './../../../../models/type';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TypeService } from '../../../../services/type.service';
import { Guid } from "guid-typescript";

@Component({
  selector: 'app-add-type',
  templateUrl: './add-type.component.html',
  styleUrls: ['./add-type.component.css']
})
export class AddTypeComponent implements OnInit {
  closeBtnName: string;
  registerForm: FormGroup;
  Types: Type[];
  type: Type = new Type(null, null, '');
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(
    public bsModalRef: BsModalRef,
    private typeService: TypeService,
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

  get t() {
    return this.registerForm.controls;
  }

  public submit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.type.Id = Guid.create().toString();
    // falta ver como seleccionar el area
    this.typeService.add(this.type).subscribe(
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
