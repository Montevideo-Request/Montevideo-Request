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
    request: {};
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
                var control: FormControl;
                if( field.fieldType == 'Boolean' ) 
                {
                    control = new FormControl("");
                    this.requestForm.addControl(field.id, control);
                } 
                else if((field.fieldType == 'Fecha' || field.fieldType == 'Entero') && field.multiSelect )
                {
                    field.ranges.forEach(range => {
                        var controlRanges: FormControl = new FormControl("");
                        this.requestForm.addControl(range.id, controlRanges);       
                    });
                }
                else
                {
                    control = new FormControl("", Validators.required);
                    this.requestForm.addControl(field.id, control);
                }
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

    yourMethod(event: string){
        // debugger
    }

    serializeRequest() {
        var request = {};
        request["AdditionalFieldValues"] = [];
        Object.keys(this.requestForm.value).forEach(field => {
            this.additionalFields.forEach(adField => {
                var additionalFieldValue = 
                {
                    "AdditionalFieldId": adField.id ,
                    "Values": []
                }

                if (adField.id == field) {
                    var value = 
                    { "Value": this.requestForm.value[field] };
                    additionalFieldValue.Values.push(value);
                    request["AdditionalFieldValues"].push(additionalFieldValue);
                }

                adField.ranges.forEach(range => {
                    if(range.id == field ) {
                        var value;
                        if(adField.fieldType == 'Fecha'){
                            value = { "Value": this.formatDate(this.requestForm.value[field])};
                        }
                        else
                        {
                            value = { "Value": this.requestForm.value[field] };
                        }

                        additionalFieldValue["Values"].push(value);
                        if(Object.keys(request["AdditionalFieldValues"]).length){
                            for(var i = 0; i < Object.keys(request["AdditionalFieldValues"]).length; i ++)
                            { 
                                
                                if (adField.id == request["AdditionalFieldValues"][i].AdditionalFieldId){
                                    request["AdditionalFieldValues"][i]["Values"].push(value)
                                }
                                
                            }
                        }else{
                            request["AdditionalFieldValues"].push(additionalFieldValue);   
                        }
                    }
                });

            });
            if (!this.additionalFields.find( x => x.id == field)) {
                request[field] = this.requestForm.value[field];
            }
        });

        request["TypeId"] = this.selectedType.id;
        request["Type"] = this.selectedType;
        console.log('request', request);
        console.log('form', this.requestForm.value);
        return request;
    }

    formatDate(dateToformat: string)
    {
        var format = dateToformat.split('-');
        return format[1] + '/' + format[2] + '/' + format[0];
    }
    
    public SubmitRequest() {
        // this.submitted = true;

        this.request = this.serializeRequest();
        if (this.requestForm.invalid) {
            return;
        }

        this.requestService.add(this.request).subscribe((result: {}) => { 
            this.request["id"] = result["id"];
            this.submitted = true;
         },
            (error: any) => {
                this.errorMessage = error;
                this.error = true;
            }
        );
    }
}
