import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { JumbotronComponent } from './jumbotron/jumbotron.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { RoutingModule } from './routing/routing.module';
import { BreweriesComponent } from './breweries/breweries.component';
import { BreweryComponent } from './brewery/brewery.component';
import { BreweryWrapperComponent } from './brewery-wrapper/brewery-wrapper.component';
import { MapComponent } from './map/map.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    JumbotronComponent,
    FooterComponent,
    BreweriesComponent,
    BreweryComponent,
    BreweryWrapperComponent,
    MapComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    RoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDwBlrTeJ4gVlxiO6La_HFALo8RUyHCtgY'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
