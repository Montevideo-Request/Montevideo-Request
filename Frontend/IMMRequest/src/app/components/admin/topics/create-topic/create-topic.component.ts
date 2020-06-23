
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TopicService } from '../../../../services/topic.service';
import { AreaService } from '../../../../services/area.service';
import { Guid } from "guid-typescript";
import { Topic } from '../../../../models/topic';
import { Area } from './../../../../models/area';

@Component({
  selector: 'app-create-topic',
  templateUrl: './create-topic.component.html',
  styleUrls: ['./create-topic.component.css']
})
export class CreateTopicComponent implements OnInit {
  response: any = { keys: "", body: "" };
  faUserPlus = faUserPlus;
  faUserEdit = faUserEdit;
  faUserRemove = faUserSlash;
  faSearch = faSearch;
  closeBtnName: string;
  registerForm: FormGroup;
  topics: Topic[];
  areas: Area[];
  topic: Topic = new Topic(null, null, '', null);
  selectedArea = '';
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(
    public bsModalRef: BsModalRef,
    private areaService: AreaService,
    private topicService: TopicService,
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

    this.areaService
      .getAreas()
      .subscribe((areas: Area[]) => this.areas = areas, messageError => this.response.body = messageError);
  }

  get a() {
    return this.registerForm.controls;
  }

  public submit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.topic.id = Guid.create().toString();
    this.areas.forEach(function (data) {
      if (data.name == this.selectedArea) {
        this.topic.areaId = data.id;
      }
    }.bind(this));
    this.topicService.add(this.topic).subscribe(
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
