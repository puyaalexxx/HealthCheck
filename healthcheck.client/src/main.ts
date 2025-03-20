import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter, Routes } from '@angular/router';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HomeComponent } from './app/home/home.component';
import { FetchDataComponent } from './app/fetch-data/fetch-data.component';
import { HealthCheckComponent } from './app/health-check/health-check.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'health-check', component: HealthCheckComponent },
];

// Bootstrap the application
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),  // Use the imported routes array here
    { provide: LocationStrategy, useClass: PathLocationStrategy }, provideAnimationsAsync(), // Use HashLocationStrategy
  ],
}).catch((err) => console.error(err));
