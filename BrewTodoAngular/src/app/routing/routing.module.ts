import { BreweryWrapperComponent } from './../brewery-wrapper/brewery-wrapper.component';
import { JumbotronComponent } from './../jumbotron/jumbotron.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from "@angular/router";
import { BreweriesComponent } from '../breweries/breweries.component'

const appRoutes: Routes = [
  { path: 'breweries', component: BreweriesComponent},
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: JumbotronComponent },
  { path: 'brewery/:id', component: BreweryWrapperComponent }
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes)
  ],
  declarations: [],
  exports: [RoutingModule]
})
export class RoutingModule { }
