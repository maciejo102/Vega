import { PhotoService } from './services/photo.service';
import { YesNoPipe } from './components/shared/custom-pipes/yes-no.pipe';
import { PaginationComponent } from './components/shared/pagination.component';
import { AppErrorHandler } from './components/app/app.error-handler';
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import * as Raven from 'raven-js';

import { VehicleService } from './services/vehicle.service';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { VehicleViewComponent } from './components/vehicle-view/vehicle-view.component';

Raven
  .config('https://09405f64e6a74cc5adcfc0cf740084d4@sentry.io/1260893')
  .install();

@NgModule({
    declarations: [
        AppComponent,
        VehicleViewComponent,
        YesNoPipe,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        VehicleListComponent,
        PaginationComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: VehicleViewComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: '**', redirectTo: 'home,' }
        ])
    ],
    providers: [
        {provide: ErrorHandler, useClass: AppErrorHandler },
        VehicleService,
        PhotoService
    ]
    
})
export class AppModuleShared {
}
