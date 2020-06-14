import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { FooterComponent } from './components/footer/footer.component';
import { ReportsComponent } from './components/admin/reports/reports.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ManageAdministratorsComponent } from './components/admin/administrators/manage-administrators/manage-administrators.component';
import { RequestComponent } from './components/request/request.component';
import { AreasComponent } from './components/areas/areas.component';
import { TypesComponent } from './components/types/types.component';
import { TopicsComponent } from './components/topics/topics.component';
import { AdditionalFieldsComponent } from './components/additional-fields/additional-fields.component';
import { PanelComponent } from './components/panel/panel.component';
import { ManageAreasComponent } from './components/admin/areas/manage-areas/manage-areas.component';
import { ManageTopicsComponent } from './components/admin/topics/manage-topics/manage-topics.component';
import { ManageTypesComponent } from './components/admin/types/manage-types/manage-types.component';
import { ManageAdditionalFieldsComponent } from './components/admin/additional-fields/manage-additional-fields/manage-additional-fields.component';
import { AdministratorListFilterPipe } from './pipes/administrator-list-filter.pipe';
import { AreasListFilterPipe } from './pipes/areas-list-filter.pipe';
import { TopicsListFilterPipe } from './pipes/topics-list-filter.pipe';
import { TypesListFilterPipe } from './pipes/types-list-filter.pipe';
import { AdditionalFieldsListFilterPipe } from './pipes/additional-fields-list-filter.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    LogoutComponent,
    FooterComponent,
    ReportsComponent,
    NavbarComponent,
    WelcomeComponent,
    ManageAdministratorsComponent,
    RequestComponent,
    AreasComponent,
    TypesComponent,
    TopicsComponent,
    AdditionalFieldsComponent,
    PanelComponent,
    ManageAreasComponent,
    ManageTopicsComponent,
    ManageTypesComponent,
    ManageAdditionalFieldsComponent,
    AdministratorListFilterPipe,
    AreasListFilterPipe,
    TopicsListFilterPipe,
    TypesListFilterPipe,
    AdditionalFieldsListFilterPipe
  ],
  imports: [
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatCardModule,
    MatToolbarModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
