import { Type } from './../../../../models/type';
import { Topic } from './../../../../models/topic';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TypeService } from '../../../../services/type.service';
import { TopicService } from '../../../../services/topic.service';
import { Guid } from "guid-typescript";

@Component({
    selector: 'app-add-type',
    templateUrl: './add-type.component.html',
    styleUrls: ['./add-type.component.css']
})
export class AddTypeComponent implements OnInit {
    selectedTopic = '';
    response: any = { keys: "", body: "" };
    closeBtnName: string;
    registerForm: FormGroup;
    types: Type[];
    topics: Topic[];
    type: Type = new Type(null, null, '');
    submitted = false;
    error = false;
    errorMessage = '';
    constructor(
        public bsModalRef: BsModalRef,
        private typeService: TypeService,
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

        this.topicService
            .getTopics()
            .subscribe((topics: Topic[]) => this.topics = topics, messageError => this.response.body = messageError);
    }

    get t() {
        return this.registerForm.controls;
    }

    public submit() {
        this.submitted = true;
        if (this.registerForm.invalid) {
            return;
        }
        this.type.id = Guid.create().toString();
        this.topics.forEach(function (data) {
            if (data.name == this.selectedTopic) {
                this.type.topicId = data.id;
            }
        }.bind(this));
        this.typeService.add(this.type).subscribe(
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
