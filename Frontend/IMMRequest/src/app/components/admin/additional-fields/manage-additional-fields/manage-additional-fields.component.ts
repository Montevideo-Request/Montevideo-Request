import { AddAdditionalFieldComponent } from '../add-additional-field/add-additional-field.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditAdditionalFieldComponent } from '../edit-additional-field/edit-additional-field.component';

import { AdditionalField } from './../../../../models/additionalField';
import { Type } from './../../../../models/type';

import { AdditionalFieldService } from '../../../../services/additional-field.service';
import { TypeService } from '../../../../services/type.service';
import { Guid } from 'guid-typescript';
import { FieldRange } from 'src/app/models/fieldRange';

@Component({
  selector: 'app-manage-additional-fields',
  templateUrl: './manage-additional-fields.component.html',
  styleUrls: ['./manage-additional-fields.component.css']
})
export class ManageAdditionalFieldsComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  additionalFields: AdditionalField[];
  additionalField: AdditionalField;
  fieldRanges: FieldRange[];
  types: Type[];
  type: Type;
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private additionalFieldService: AdditionalFieldService,
    private typeService: TypeService
  ) { }

  ngOnInit() {
    this.modalService.onHide.subscribe(() => {
      this.additionalFieldService
        .getAdditionalFields()
        .subscribe((additionalFields: AdditionalField[]) =>
          this.additionalFields = additionalFields, messageError => this.response.body = messageError);
    });
    this.additionalFieldService
      .getAdditionalFields()
      .subscribe((additionalFields: AdditionalField[]) =>
        this.additionalFields = additionalFields, messageError => this.response.body = messageError);
    this.typeService
      .getTypes()
      .subscribe((types: Type[]) => this.types = types, messageError => this.response.body = messageError);
  }

  edit(item: AdditionalField) {
    this.openEditModal(item);
  }

  delete(additionalField: AdditionalField): void {
    this.additionalFields = this.additionalFields.filter(h => h !== additionalField);
    this.additionalFieldService.delete(additionalField).subscribe();
  }

  add(): void {
    this.openRegisterModal();
  }

  openRegisterModal() {
    this.bsModalRef = this.modalService.show(AddAdditionalFieldComponent, {
      class: 'modal-lg'
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  openEditModal(item: AdditionalField) {
    const initialState = {
      additionalField: item
    };
    this.bsModalRef = this.modalService.show(EditAdditionalFieldComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  getRanges(item: AdditionalField) {

  }
}
