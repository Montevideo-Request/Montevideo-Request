import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

import { RequestService } from '../../services/request.service';
import { TypeService } from '../../services/type.service';

import { Request } from './../../models/request';
import { Type } from './../../models/type';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {
  types: Type[];
  requestForm: FormGroup;
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  faSpinnner = faSpinner;
  request: Request = new Request(null, null, '', '', '', '', null);
  submitted = false;
  error = false;
  errorMessage = '';
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private requestService: RequestService,
    private typeService: TypeService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.requestForm = this.formBuilder.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^[a-zA-Z ]*$')
        ])
      ],
      phone: ['', Validators.compose([Validators.required])],
      email: ['', Validators.compose([Validators.required, Validators.email])]
    });

    this.typeService
       .getTypes()
       .subscribe((types: Type[]) => this.types = types, messageError => this.response.body = messageError);

    // this.administratorService
    //   .GetAll()
    //   .subscribe((users: AdministratorBasicInfo[]) => this.administrators = users, messageError => this.response.body = messageError);

    // this.administratorService
    //   .GetAll()
    //   .subscribe((users: AdministratorBasicInfo[]) => this.administrators = users, messageError => this.response.body = messageError);
  }

  get r() {
    return this.requestForm.controls;
  }

  public submit() {
    this.submitted = true;
    if (this.requestForm.invalid) {
      return;
    }
    this.request.Id = Guid.create().toString();
    console.log(this.request);
    this.requestService.add(this.request).subscribe(
      () => {},
      (error: any) => {
        this.errorMessage = error;
        this.error = true;
      }
    );
  }
}
