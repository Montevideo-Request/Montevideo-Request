import { Topic } from './../../../../models/topic';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TopicService } from '../../../../services/topic.service';

@Component({
  selector: 'app-edit-topic',
  templateUrl: './edit-topic.component.html',
  styleUrls: ['./edit-topic.component.css']
})
export class EditTopicComponent implements OnInit {
  topic: Topic;
  closeBtnName: string;
  editForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  constructor(public bsModalRef: BsModalRef, private topicService: TopicService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required])]
    });
  }

  get a() { return this.editForm.controls; }

  public submit() {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }
    this.topicService.edit(this.topic).subscribe(
      () => {
        this.bsModalRef.hide();
      },
      ((error: any) => {
        this.errorMessage = error;
        this.error = true;
      })
    );
  }

  onClosed(): void {
  // onClosed(dismissedAlert: AlertComponent): void {
    this.error = false;
  }

}
