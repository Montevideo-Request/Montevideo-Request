import { AddTypeComponent } from '../add-type/add-type.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditTypeComponent } from '../edit-type/edit-type.component';
import { Subscription } from 'rxjs';

import { Type } from './../../../../models/type';
import { Topic } from './../../../../models/topic';

import { TypeService } from '../../../../services/type.service';
import { TopicService } from '../../../../services/topic.service';

@Component({
  selector: 'app-dashboard-types',
  templateUrl: './dashboard-types.component.html',
  styleUrls: ['./dashboard-types.component.css']
})
export class DashboardTypesComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  types: Type[];
  type: Type;
  topics: Topic[];
  topic: Topic;
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private typeService: TypeService,
    private topicService: TopicService
  ) { }

  ngOnInit() {
    this.modalService.onHide.subscribe(() => {
      this.typeService
        .getTypes()
        .subscribe((types: Type[]) => this.types = types, messageError => this.response.body = messageError);
    });
    this.typeService
      .getTypes()
      .subscribe((types: Type[]) => this.types = types, messageError => this.response.body = messageError);
    this.topicService
      .getTopics()
      .subscribe((topics: Topic[]) => this.topics = topics, messageError => this.response.body = messageError);
  }

  edit(item: Type) {
    this.openEditModal(item);
  }

  delete(type: Type): void {
    this.types = this.types.filter(h => h !== type);
    this.typeService.delete(type).subscribe();
  }

  add(): void {
    this.openRegisterModal();
  }

  openRegisterModal() {
    this.bsModalRef = this.modalService.show(AddTypeComponent, {
      class: 'modal-lg'
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  openEditModal(item: Type) {
    const initialState = {
      type: item
    };
    this.bsModalRef = this.modalService.show(EditTypeComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}
