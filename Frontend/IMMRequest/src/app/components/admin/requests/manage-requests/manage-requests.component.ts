import { Request } from './../../../../models/request';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EditRequestComponent } from '../edit-request/edit-request.component';
import { Subscription } from 'rxjs';
import { RequestService } from '../../../../services/request.service';

@Component({
  selector: 'app-manage-requests',
  templateUrl: './manage-requests.component.html',
  styleUrls: ['./manage-requests.component.css']
})
export class ManageRequestsComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  requests: Request[];
  request: Request;
  subscriptions: Subscription[] = [];
  bsModalRef: BsModalRef;
  listFilter: '';
  constructor(
    private modalService: BsModalService,
    private changeDetection: ChangeDetectorRef,
    private requestService: RequestService
  ) { }

  ngOnInit() {
    this.modalService.onHide.subscribe(() => {
      this.requestService
        .getRequests()
        .subscribe((requests: Request[]) => this.requests = requests, messageError => this.response.body = messageError);
    });
    this.requestService
      .getRequests()
      .subscribe((requests: Request[]) => this.requests = requests, messageError => this.response.body = messageError);

  }

  edit(item: Request) {
    this.openEditModal(item);
  }

  delete(request: Request): void {
    this.requests = this.requests.filter(h => h !== request);
    this.requestService.delete(request).subscribe();
  }

  openEditModal(item: Request) {
    const initialState = {
      administrator: item
    };
    this.bsModalRef = this.modalService.show(EditRequestComponent, {
      class: 'modal-lg',
      initialState
    });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}
