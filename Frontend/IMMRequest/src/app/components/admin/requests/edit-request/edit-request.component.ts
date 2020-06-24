import { Request } from './../../../../models/request';
import { AlertComponent } from 'ngx-bootstrap/alert/alert.component';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { RequestService } from '../../../../services/request.service';

@Component({
  selector: 'app-edit-request',
  templateUrl: './edit-request.component.html',
  styleUrls: ['./edit-request.component.css']
})
export class EditRequestComponent implements OnInit {
  response: any = { keys: "", body: "" };
  request: Request;
  closeBtnName: string;
  editForm: FormGroup;
  submitted = false;
  error = false;
  errorMessage = '';
  states: [];
  currentState: string;
  description: string;
  constructor(public bsModalRef: BsModalRef, private requestService: RequestService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.editForm = this.formBuilder.group({
        State: [this.currentState, [Validators.required]],
        Description: new FormControl(this.request.description)
      });
    
    this.requestService.GetStates()
    .subscribe((states: []) => {
        this.states = states
        this.currentState = this.request.state;

    }, messageError => this.response.body = messageError);
  }

  get controls() { return this.editForm.controls; } 

  stateSelected(state: string){
    this.editForm.controls['State'].setValue(state['value']);
  }

  public submit() {
    this.submitted = true;

    if (this.editForm.invalid) { return; }
    
    this.requestService.edit(this.editForm.value, this.request.id).subscribe(
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
    this.error = false;
  }
}
