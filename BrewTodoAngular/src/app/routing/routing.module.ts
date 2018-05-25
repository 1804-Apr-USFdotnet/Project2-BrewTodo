import { JumbotronComponent } from './../jumbotron/jumbotron.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from "@angular/router";
import { BreweriesComponent } from '../breweries/breweries.component'

const appRoutes: Routes = [
  { path: 'breweries', component: BreweriesComponent},
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: JumbotronComponent }
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes)
  ],
  declarations: []
})
export class RoutingModule { }
