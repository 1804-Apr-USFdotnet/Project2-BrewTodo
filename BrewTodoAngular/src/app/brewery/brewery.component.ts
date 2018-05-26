import { Brewery } from './../models/brewery';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Routes} from "@angular/router";
import { BreweriesService } from './../breweries.service';
import { trigger,state,style,transition,animate,keyframes } from '@angular/animations';

@Component({
  selector: 'app-brewery',
  templateUrl: './brewery.component.html',
  styleUrls: ['./brewery.component.css']
})
export class BreweryComponent implements OnInit {

  @Input() brewery: Brewery;

  constructor() {}

  ngOnInit(){}


}
