import { AdministratorBasicInfo } from './../../../../models/administratorBasicInfo';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdministratorService } from '../../../../services/administrator.service';

@Component({
  selector: 'app-edit-administrator',
  templateUrl: './edit-administrator.component.html',
  styleUrls: ['./edit-administrator.component.css']
})
export class EditAdministratorComponent implements OnInit {
  administrator: AdministratorBasicInfo;
  closeBtnName: string;
  editForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(public bsModalRef: BsModalRef, private administratorService: AdministratorService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required, Validators.pattern('^[a-zA-Z ]*$')])],
      email: ['', Validators.compose([Validators.required, Validators.email])]
    });
  }

  get a() { return this.editForm.controls; }

  public submit() {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }
    this.administratorService.edit(this.administrator).subscribe(
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
