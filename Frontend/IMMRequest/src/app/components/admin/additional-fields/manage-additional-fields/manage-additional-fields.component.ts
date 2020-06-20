import { AdditionalField } from './../../../../models/additionalField';
import { AddAdditionalFieldComponent } from '../add-additional-field/add-additional-field.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditAdditionalFieldComponent } from '../edit-additional-field/edit-additional-field.component';
import { Subscription } from 'rxjs';
import { AdditionalFieldService } from '../../../../services/additional-field.service';

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
  subscriptions: Subscription[] = [];
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private additionalFieldService: AdditionalFieldService
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
      administrator: item
    };
    this.bsModalRef = this.modalService.show(EditAdditionalFieldComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}
