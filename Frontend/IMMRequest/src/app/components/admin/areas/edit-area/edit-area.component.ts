
import { Area } from './../../../../models/area';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AreaService } from '../../../../services/area.service';

@Component({
  selector: 'app-edit-area',
  templateUrl: './edit-area.component.html',
  styleUrls: ['./edit-area.component.css']
})
export class EditAreaComponent implements OnInit {
  area: Area;
  closeBtnName: string;
  editForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(public bsModalRef: BsModalRef, private areaService: AreaService, private formBuilder: FormBuilder) { }

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
    console.log(this.area);
    this.areaService.edit(this.area).subscribe(
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
