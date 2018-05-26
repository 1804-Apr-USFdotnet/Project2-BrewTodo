import { Beers } from './../models/beers';
import { Reviews } from './../models/reviews';
import { Brewery } from './../models/brewery';
import { Component, OnInit, Input, NgZone } from '@angular/core';

@Component({
  selector: 'app-beer',
  templateUrl: './beer.component.html',
  styleUrls: ['./beer.component.css']
})
export class BeerComponent implements OnInit {

  @Input() brewery: Brewery;
  beers;
  constructor(private zone:NgZone ) { }

  ngOnInit() {

  }
  ngAfterViewInit(){
    this.beers = this.brewery.Beers;
    console.log(this.beers);
  }

}
