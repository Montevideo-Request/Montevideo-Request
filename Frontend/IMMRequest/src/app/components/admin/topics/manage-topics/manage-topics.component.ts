import { Topic } from './../../../../models/topic';
import { CreateTopicComponent } from '../create-topic/create-topic.component';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditTopicComponent } from '../edit-topic/edit-topic.component';
import { TopicService } from '../../../../services/topic.service';

@Component({
  selector: 'app-manage-topics',
  templateUrl: './manage-topics.component.html',
  styleUrls: ['./manage-topics.component.css']
})
export class ManageTopicsComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  topics: Topic[];
  topic: Topic;
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private topicService: TopicService
  ) { }

  ngOnInit() {
    this.modalService.onHide.subscribe(() => {
      this.topicService
        .getTopics()
        .subscribe((topics: Topic[]) => this.topics = topics, messageError => this.response.body = messageError);
    });
    this.topicService
      .getTopics()
      .subscribe((topics: Topic[]) => this.topics = topics, messageError => this.response.body = messageError);

  }

  edit(item: Topic) {
    this.openEditModal(item);
  }

  delete(topic: Topic): void {
    this.topics = this.topics.filter(h => h !== topic);
    this.topicService.delete(topic).subscribe();
  }

  add(): void {
    this.openRegisterModal();
  }

  openRegisterModal() {
    this.bsModalRef = this.modalService.show(CreateTopicComponent, {
      class: 'modal-lg'
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  openEditModal(item: Topic) {
    const initialState = {
      topic: item
    };
    this.bsModalRef = this.modalService.show(EditTopicComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}
