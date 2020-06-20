import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { PanelComponent } from './components/panel/panel.component';

import { NavbarComponent } from './components/navbar/navbar.component';
import { ManageAdministratorsComponent } from './components/admin/administrators/manage-administrators/manage-administrators.component';
import { ManageRequestsComponent } from './components/admin/requests/manage-requests/manage-requests.component';
import { ManageAreasComponent } from './components/admin/areas/manage-areas/manage-areas.component';
import { ManageTopicsComponent } from './components/admin/topics/manage-topics/manage-topics.component';
import { DashboardTypesComponent } from './components/admin/types/dashboard-types/dashboard-types.component';
import { ManageAdditionalFieldsComponent } from './components/admin/additional-fields/manage-additional-fields//manage-additional-fields.component';

import { AdditionalFieldsComponent } from './components/additional-fields/additional-fields.component';
import { TypesComponent } from './components/types/types.component';
import { TopicsComponent } from './components/topics/topics.component';
import { AreasComponent } from './components/areas/areas.component';
import { RequestComponent } from './components/request/request.component';

import { ReportsComponent } from './components/admin/reports/reports.component';
import { TypeAComponent } from './components/admin/reports/type-a/type-a.component';
import { TypeBComponent } from './components/admin/reports/type-b/type-b.component';

import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';

import { AuthenticationErrorComponent } from './components/error/authentication-error/authentication-error.component';
import { NotFoundComponent } from './components/error/not-found/not-found.component';

import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';
import { LoginGuard } from './guards/login.guard';
import { ParserComponent } from './components/parser/parser.component';

export const routes: Routes = [
    // { path: 'admin', component: HomeComponent },
    //   {path: 'request/:id', component: RequestComponent, canActivate: [AuthGuard]},
    {
        path: '',
        component: LoginComponent,
        canActivate: [LoginGuard]
    },
    {
        path: 'admin/administrators',
        component: ManageAdministratorsComponent
    },
    {
        path: 'admin/requests',
        component: ManageRequestsComponent
    },
    {
        path: 'admin/areas',
        component: ManageAreasComponent
    },
    {
        path: 'admin/types',
        component: DashboardTypesComponent
    },
    {
        path: 'admin/topics',
        component: ManageTopicsComponent
    },
    {
        path: 'admin/additionalFields',
        component: ManageAdditionalFieldsComponent
    },
    {
        path: 'admin/reports/typeB',
        component: TypeBComponent
    },
    {
        path: 'admin/reports/typeA',
        component: TypeAComponent
    },
    {
        path: 'report',
        component: ReportsComponent
    },
    {
        path: 'request',
        component: RequestComponent
    },
    {
        path: 'parser',
        component: ParserComponent
    },
    {
        path: 'logout',
        component: LogoutComponent
    },
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: '**',
        redirectTo: '/home'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
