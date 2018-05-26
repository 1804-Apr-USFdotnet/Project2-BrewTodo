import { BreweriesService } from './../breweries.service';
import { Brewery } from './../models/brewery';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-breweries',
  templateUrl: './breweries.component.html',
  styleUrls: ['./breweries.component.css']
})
export class BreweriesComponent implements OnInit {

  breweries: Brewery[];

  constructor(private brewerySvc: BreweriesService) { }

  ngOnInit() {
    this.getAllBreweries();
  }

  getAllBreweries(){
    this.brewerySvc.getBreweries(response =>{
      this.breweries = response;
    });
  }


}
