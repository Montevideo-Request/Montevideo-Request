import { Administrator } from './../../../../models/administrator';
import { AdministratorBasicInfo } from './../../../../models/administratorBasicInfo';
import { AddAdministratorComponent } from '../add-administrator/add-administrator.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditAdministratorComponent } from '../edit-administrator/edit-administrator.component';
import { Subscription } from 'rxjs';
import { AdministratorService } from '../../../../services/administrator.service';

@Component({
  selector: 'app-manage-administrators',
  templateUrl: './manage-administrators.component.html',
  styleUrls: ['./manage-administrators.component.css']
})
export class ManageAdministratorsComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  administrators: AdministratorBasicInfo[];
  administrator: Administrator;
  subscriptions: Subscription[] = [];
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private administratorService: AdministratorService
  ) { }

  ngOnInit() {
    this.modalService.onHide.subscribe(() => {
      this.administratorService
        .GetAll()
        .subscribe((users: AdministratorBasicInfo[]) => this.administrators = users, messageError => this.response.body = messageError);
    });
    this.administratorService
      .GetAll()
      .subscribe((users: AdministratorBasicInfo[]) => this.administrators = users, messageError => this.response.body = messageError);

  }

  edit(item: Administrator) {
    this.openEditModal(item);
  }

  delete(administrator: AdministratorBasicInfo): void {
    this.administrators = this.administrators.filter(h => h !== administrator);
    this.administratorService.delete(administrator).subscribe();
  }

  add(): void {
    this.openRegisterModal();
  }

  openRegisterModal() {
    this.bsModalRef = this.modalService.show(AddAdministratorComponent, {
      class: 'modal-lg'
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  openEditModal(item: Administrator) {
    const initialState = {
      administrator: item
    };
    this.bsModalRef = this.modalService.show(EditAdministratorComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}
