import { Area } from './../../../../models/area';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AreaService } from '../../../../services/area.service';
import { Guid } from "guid-typescript";

@Component({
  selector: 'app-add-area',
  templateUrl: './add-area.component.html',
  styleUrls: ['./add-area.component.css']
})
export class AddAreaComponent implements OnInit {
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  closeBtnName: string;
  registerForm: FormGroup;
  areas: Area[];
  area: Area = new Area(null, '');
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(
    public bsModalRef: BsModalRef,
    private areaService: AreaService,
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
    this.area.Id = Guid.create().toString();
    this.areaService.add(this.area).subscribe(
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