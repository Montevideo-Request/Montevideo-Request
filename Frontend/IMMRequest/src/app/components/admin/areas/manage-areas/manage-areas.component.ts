import { Area } from './../../../../models/area';
import { AddAreaComponent } from '../add-area/add-area.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditAreaComponent } from '../edit-area/edit-area.component';
import { Subscription } from 'rxjs';
import { AreaService } from '../../../../services/area.service';

@Component({
  selector: 'app-manage-areas',
  templateUrl: './manage-areas.component.html',
  styleUrls: ['./manage-areas.component.css']
})
export class ManageAreasComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  areas: Area[];
  area: Area;
  subscriptions: Subscription[] = [];
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private areaService: AreaService
  ) { }

  ngOnInit() {
    this.modalService.onHide.subscribe(() => {
      this.areaService
        .getAreas()
        .subscribe((areas: Area[]) => this.areas = areas, messageError => this.response.body = messageError);
    });
    this.areaService
      .getAreas()
      .subscribe((areas: Area[]) => this.areas = areas, messageError => this.response.body = messageError);

  }

  edit(item: Area) {
    this.openEditModal(item);
  }

  delete(area: Area): void {
    this.areas = this.areas.filter(h => h !== area);
    this.areaService.delete(area).subscribe();
  }

  add(): void {
    this.openRegisterModal();
  }

  openRegisterModal() {
    this.bsModalRef = this.modalService.show(AddAreaComponent, {
      class: 'modal-lg'
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  openEditModal(item: Area) {
    const initialState = {
      administrator: item
    };
    this.bsModalRef = this.modalService.show(EditAreaComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}
