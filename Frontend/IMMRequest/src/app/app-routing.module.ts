import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageAdministratorsComponent } from './components/admin/administrators/manage-administrators/manage-administrators.component';
import { ManageRequestsComponent } from './components/admin/requests/manage-requests/manage-requests.component';
import { ManageAreasComponent } from './components/admin/areas/manage-areas/manage-areas.component';
import { ManageTopicsComponent } from './components/admin/topics/manage-topics/manage-topics.component';
import { DashboardTypesComponent } from './components/admin/types/dashboard-types/dashboard-types.component';
import { ManageAdditionalFieldsComponent } from './components/admin/additional-fields/manage-additional-fields//manage-additional-fields.component';
import { RequestComponent } from './components/request/request.component';
import { ReportsComponent } from './components/admin/reports/reports.component';
import { TypeAComponent } from './components/admin/reports/type-a/type-a.component';
import { TypeBComponent } from './components/admin/reports/type-b/type-b.component';
import { WelcomeComponent } from './components/welcome/welcome.component';

import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';

import { AuthenticationErrorComponent } from './components/error/authentication-error/authentication-error.component';
import { NotFoundComponent } from './components/error/not-found/not-found.component';

import { AuthGuard } from './guards/auth.guard';
import { ParserComponent } from './components/parser/parser.component';

export const routes: Routes = [
    {
        path: '',
        component: LoginComponent
    },
    {
        path: 'admin/administrators',
        component: ManageAdministratorsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/requests',
        component: ManageRequestsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/areas',
        component: ManageAreasComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/types',
        component: DashboardTypesComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/topics',
        component: ManageTopicsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/additionalFields',
        component: ManageAdditionalFieldsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/reports/typeB',
        component: TypeBComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin/reports/typeA',
        component: TypeAComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'report',
        component: ReportsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'request',
        component: RequestComponent
    },
    {
        path: 'parser',
        component: ParserComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'welcome',
        component: WelcomeComponent,
        canActivate: [AuthGuard]
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
