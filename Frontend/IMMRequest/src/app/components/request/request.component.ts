import { Component, OnInit } from '@angular/core';
import { faUserPlus, faSearch } from '@fortawesome/free-solid-svg-icons';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';
import { faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { FormGroup, FormBuilder, Validators , FormControl} from '@angular/forms';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { RequestService } from '../../services/request.service';
import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { Request } from './../../models/request';
import { Type } from './../../models/type';
import { AreaService } from 'src/app/services/area.service';
import { TopicService } from 'src/app/services/topic.service';
import { TypeService } from 'src/app/services/type.service';
import { Area } from 'src/app/models/area';
import { Topic } from 'src/app/models/topic';
import { AdditionalField } from 'src/app/models/additionalField';
import { AdditionalFieldService } from 'src/app/services/additional-field.service';

@Component({
    selector: 'app-request',
    templateUrl: './request.component.html',
    styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {
    additionalFields: AdditionalField[];
    types: Type[];
    topics: Topic[];
    areas: Area[];
    requestForm: FormGroup;
    response: any = { keys: "", body: "" };
    request: Request;
    submitted = false;
    error = false;
    errorMessage = '';
    selectedArea: Area;
    selectedTopic: Topic;
    selectedType: Type;
    hasAdditionalFields: boolean = false;
    constructor(
        private additionalFieldService: AdditionalFieldService,
        private requestService: RequestService,
        private topicService: TopicService,
        private typeService: TypeService,
        private areaService: AreaService,
        private formBuilder: FormBuilder,
        calendar: NgbCalendar
    ){ }

    ngOnInit() {
        this.requestForm = this.formBuilder.group({
            Area: ['', [Validators.required]],
            Topic: ['', [Validators.required]],
            Type: ['', [Validators.required]],
            RequestorsName: [ '', Validators.compose([
                    Validators.required,
                    Validators.pattern('^[a-zA-Z ]*$')
                ])
            ],
            RequestorsPhone: ['', Validators.compose([Validators.required])],
            RequestorsEmail: ['', Validators.compose([Validators.required, Validators.email])]
        });

        this.areaService.getAreas().subscribe((areas: Area[]) => {
            this.areas = areas;
        }, messageError => this.response.body = messageError);
    }c

    selectArea(event: string) {
        this.requestForm.controls['Area'].setValue(event['value']);
        this.selectedArea = this.areas.find(area => area.name == this.requestForm.value["Area"]);
        this.topicService.getTopics().subscribe((topics: Topic[]) => {
            this.topics = topics.filter(topic => topic.areaId == this.selectedArea.id);
            this.error = !this.topics;
            this.errorMessage = "No Topics Found for this Area."
        }, messageError => this.response.body = messageError);
    }

    selectTopic(event: string) {
        this.requestForm.controls['Topic'].setValue(event['value']);
        this.selectedTopic = this.topics.find(topic => topic.name == this.requestForm.value["Topic"]);
        this.types = this.selectedTopic.types;

        this.typeService.getTypes().subscribe((types: Type[]) => {
            this.types = types.filter(type => type.topicId == this.selectedTopic.id);
            this.error = !this.types.length;
            this.errorMessage = "No Types Found for this Topic."
        }, messageError => this.response.body = messageError);
    }

    selectType(event: string) {
        this.requestForm.controls['Type'].setValue(event['value']);
        this.selectedType = this.types.find(type => type.name == this.requestForm.value["Type"]);
        
        this.additionalFieldService.getAdditionalFields().subscribe((fields: AdditionalField[]) => {
            this.additionalFields = fields.filter(field => field.typeId == this.selectedType.id);
            this.additionalFields.forEach(field => {
                var control: FormControl = field.fieldType == 'Boolean' ? control = new FormControl("") : control = new FormControl("", Validators.required);
                this.requestForm.addControl(field.id, control);
            });
            this.submitted = false;
            this.hasAdditionalFields = !!this.additionalFields.length;
        }, messageError => this.response.body = messageError);
    }

    get controls(){
        return this.requestForm.controls;
    }

    multiSelection(event: string){
        var field = event["source"].ngControl.name;
        this.requestForm.value[field] = event["value"];
    }

    serializeRequest() {
        var request = {};
        request["AdditionalFieldValues"] = [];
        Object.keys(this.requestForm.value).forEach(field => {
            if (this.additionalFields.find( x => x.id == field)) {
                
                // var additionalFieldValue = new AdditionalFieldValue(field, []);

                // this.request.additionalFieldValues.push();
            } else {
                request[field] = this.requestForm.value[field];
            }
        });

        request["TypeId"] = this.selectedType.id;
        request["Type"] = this.selectedType;
        console.log('form', this.requestForm.value);
        return request;
    }
    
    public SubmitRequest() {
        this.submitted = true;

        var request = this.serializeRequest();
        if (this.requestForm.invalid) {
            return;
        }

        this.requestService.add(request).subscribe(() => { },
            (error: any) => {
                this.errorMessage = error;
                this.error = true;
            }
        );
    }
}
