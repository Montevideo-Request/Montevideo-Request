import { Request } from './../../../../models/request';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RequestService } from '../../../../services/request.service';

@Component({
  selector: 'app-edit-request',
  templateUrl: './edit-request.component.html',
  styleUrls: ['./edit-request.component.css']
})
export class EditRequestComponent implements OnInit {
  request: Request;
  closeBtnName: string;
  editForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(public bsModalRef: BsModalRef, private requestService: RequestService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      state: ['', Validators.compose([Validators.required, Validators.pattern('^[a-zA-Z ]*$')])]
    });
  }

  get a() { return this.editForm.controls; }

  public submit() {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }
    this.requestService.edit(this.request).subscribe(
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
