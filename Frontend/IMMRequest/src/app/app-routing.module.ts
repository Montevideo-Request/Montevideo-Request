import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { PanelComponent } from './components/panel/panel.component';

import { ManageAreasComponent } from './components/admin/areas/manage-areas/manage-areas.component';
import { ManageTopicsComponent } from './components/admin/topics/manage-topics/manage-topics.component';
import { ManageTypesComponent } from './components/admin/types/manage-types/manage-types.component';
import { ManageAdditionalFieldsComponent } from './components/admin/additional-fields/manage-additional-fields//manage-additional-fields.component';
import { ManageAdministratorsComponent } from './components/admin/administrators/manage-administrators/manage-administrators.component';

import { AdditionalFieldsComponent } from './components/additional-fields/additional-fields.component';
import { TypesComponent } from './components/types/types.component';
import { TopicsComponent } from './components/topics/topics.component';
import { AreasComponent } from './components/areas/areas.component';
import { RequestComponent } from './components/request/request.component';
import { ReportsComponent } from './components/admin/reports/reports.component';

import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';

import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'request/:id', component: RequestComponent, canActivate: [AuthGuard]},
  {path: 'admin/administrators', component: ManageAdministratorsComponent, canActivate: [AdminGuard]},
  {path: 'admin/areas', component: ManageAreasComponent, canActivate: [AdminGuard]},
  {path: 'admin/topics', component: ManageTopicsComponent, canActivate: [AdminGuard]},
  {path: 'admin/types', component: ManageTypesComponent, canActivate: [AdminGuard]},
  {path: 'admin/additionalFields', component: ManageAdditionalFieldsComponent, canActivate: [AdminGuard]},
  {path: 'admin/reports', component: ReportsComponent, canActivate: [AdminGuard]},
  {path: 'admin/panel', component: PanelComponent, canActivate: [AdminGuard]},
  {path: 'areas', component: AreasComponent, canActivate: [AuthGuard]},
  {path: 'topics', component: TopicsComponent, canActivate: [AuthGuard]},
  {path: 'types', component: TypesComponent, canActivate: [AuthGuard]},
  {path: 'additionalFields', component: AdditionalFieldsComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent, canActivate: [LoginGuard]},
  {path: 'logout', component: LogoutComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full' },
  {path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
