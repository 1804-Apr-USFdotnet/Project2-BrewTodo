import { BreweriesService } from './../breweries.service';
import { Brewery } from './../models/brewery';
import { Component, OnInit } from '@angular/core';
import { trigger,state,style,transition,animate,keyframes,query,stagger } from '@angular/animations';

@Component({
  selector: 'app-breweries',
  templateUrl: './breweries.component.html',
  styleUrls: ['./breweries.component.css'],
  animations: [
    trigger('breweriesAnimation', [
      transition('* => *', [
        query('.card',style({ transform: 'translateX(-100%)'}),{optional: true}),
        query('.card',
          [stagger('200ms', [
            animate('500ms', style({ transform: 'translateX(0)'}))
        ])],{optional: true})
      ])
    ])
  ]
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
