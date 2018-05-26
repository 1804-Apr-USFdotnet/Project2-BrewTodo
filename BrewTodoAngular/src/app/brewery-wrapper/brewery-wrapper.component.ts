import { BreweriesService } from './../breweries.service';
import { ActivatedRoute } from '@angular/router';
import { Brewery } from './../models/brewery';
import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-brewery-wrapper',
  templateUrl: './brewery-wrapper.component.html',
  styleUrls: ['./brewery-wrapper.component.css']
})
export class BreweryWrapperComponent implements OnInit {

  brewID: number;
  brewery: Brewery;
  constructor(private route: ActivatedRoute, private brewerySvc: BreweriesService) { }

  ngOnInit() {
    this.route
      .queryParams
      .subscribe(params => {
        this.brewID = +params['BreweryID'];
      });
      this.getBrewery();
  }
  getBrewery(){
    this.brewerySvc.getBreweriesByID(this.brewID, (response) =>{
      this.brewery = response;
    });
}
}
