import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { JumbotronComponent } from './jumbotron/jumbotron.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HttpClient } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { RoutingModule } from './routing/routing.module';
import { BreweriesComponent } from './breweries/breweries.component';
import { BreweryComponent } from './brewery/brewery.component';
import { BreweryWrapperComponent } from './brewery-wrapper/brewery-wrapper.component';
import { MapComponent } from './map/map.component';
import { ReviewComponent } from './review/review.component';
import { BeerComponent } from './beer/beer.component';
import { provideRoutes} from '@angular/router';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    JumbotronComponent,
    FooterComponent,
    BreweriesComponent,
    BreweryComponent,
    BreweryWrapperComponent,
    MapComponent,
    ReviewComponent,
    BeerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RoutingModule,
    BrowserAnimationsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDwBlrTeJ4gVlxiO6La_HFALo8RUyHCtgY'
    })
  ],
  providers: [HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
