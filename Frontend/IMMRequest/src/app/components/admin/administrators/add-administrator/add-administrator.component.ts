import { Administrator } from './../../../../models/administrator';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdministratorService } from '../../../../services/administrator.service';
import { Guid } from "guid-typescript";

@Component({
  selector: 'app-add-administrator',
  templateUrl: './add-administrator.component.html',
  styleUrls: ['./add-administrator.component.css']
})
export class AddAdministratorComponent implements OnInit {
  closeBtnName: string;
  registerForm: FormGroup;
  administrators: Administrator[];
  administrator: Administrator = new Administrator(null, '', '', '');
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(
    public bsModalRef: BsModalRef,
    private administratorService: AdministratorService,
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
      ],
      password: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(10)
        ])
      ],
      email: ['', Validators.compose([Validators.required, Validators.email])]
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
    this.administrator.Id = Guid.create().toString();
    this.administratorService.add(this.administrator).subscribe(
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