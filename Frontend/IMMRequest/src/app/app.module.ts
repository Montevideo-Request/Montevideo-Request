import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { FooterComponent } from './components/footer/footer.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { RequestComponent } from './components/request/request.component';
import { ManageAreasComponent } from './components/admin/areas/manage-areas/manage-areas.component';
import { ManageTopicsComponent } from './components/admin/topics/manage-topics/manage-topics.component';
import { ManageAdditionalFieldsComponent } from './components/admin/additional-fields/manage-additional-fields/manage-additional-fields.component';
import { AdministratorListFilterPipe } from './pipes/administrator-list-filter.pipe';
import { AreasListFilterPipe } from './pipes/areas-list-filter.pipe';
import { TopicsListFilterPipe } from './pipes/topics-list-filter.pipe';
import { TypesListFilterPipe } from './pipes/types-list-filter.pipe';
import { AdditionalFieldsListFilterPipe } from './pipes/additional-fields-list-filter.pipe';
import { RequestsListFilterPipe } from './pipes/requests-list-filter.pipe';

import { AddAdministratorComponent } from './components/admin/administrators/add-administrator/add-administrator.component';
import { EditAdministratorComponent } from './components/admin/administrators/edit-administrator/edit-administrator.component';
import { ManageAdministratorsComponent } from './components/admin/administrators/manage-administrators/manage-administrators.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModalModule, BsModalRef } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AlertModule } from 'ngx-bootstrap/alert';
import { AddAreaComponent } from './components/admin/areas/add-area/add-area.component';
import { EditAreaComponent } from './components/admin/areas/edit-area/edit-area.component';
import { ParserComponent } from './components/parser/parser.component';
import { MatMenuModule } from '@angular/material/menu';

import { MatSelectModule } from '@angular/material/select';
import { ManageRequestsComponent } from './components/admin/requests/manage-requests/manage-requests.component';
import { EditRequestComponent } from './components/admin/requests/edit-request/edit-request.component';
import { EditTypeComponent } from './components/admin/types/edit-type/edit-type.component';
import { AddTypeComponent } from './components/admin/types/add-type/add-type.component';
import { DashboardTypesComponent } from './components/admin/types/dashboard-types/dashboard-types.component';
import { EditTopicComponent } from './components/admin/topics/edit-topic/edit-topic.component';
import { AddAdditionalFieldComponent } from './components/admin/additional-fields/add-additional-field/add-additional-field.component';
import { EditAdditionalFieldComponent } from './components/admin/additional-fields/edit-additional-field/edit-additional-field.component';

import { ReportsComponent } from './components/admin/reports/reports.component';
import { TypeAComponent } from './components/admin/reports/type-a/type-a.component';
import { TypeBComponent } from './components/admin/reports/type-b/type-b.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';
import { CreateTopicComponent } from './components/admin/topics/create-topic/create-topic.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent,
    FooterComponent,
    ReportsComponent,
    NavbarComponent,
    ManageAdministratorsComponent,
    RequestComponent,
    ManageAreasComponent,
    ManageTopicsComponent,
    ManageAdditionalFieldsComponent,
    AdministratorListFilterPipe,
    AreasListFilterPipe,
    TopicsListFilterPipe,
    TypesListFilterPipe,
    AdditionalFieldsListFilterPipe,
    AddAdministratorComponent,
    EditAdministratorComponent,
    ManageAdministratorsComponent,
    AddAreaComponent,
    EditAreaComponent,
    ParserComponent,
    ManageRequestsComponent,
    EditRequestComponent,
    RequestsListFilterPipe,
    EditTypeComponent,
    DashboardTypesComponent,
    EditTopicComponent,
    AddAdditionalFieldComponent,
    EditAdditionalFieldComponent,
    TypeAComponent,
    TypeBComponent,
    CreateTopicComponent,
    AddTypeComponent
  ],
  imports: [
    ModalModule.forRoot(),
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatCardModule,
    MatToolbarModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbModule,
    MatSelectModule,
    MatMenuModule,
    MatDatepickerModule,
    AlertModule.forRoot()
  ],
  providers: [BsModalRef, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
