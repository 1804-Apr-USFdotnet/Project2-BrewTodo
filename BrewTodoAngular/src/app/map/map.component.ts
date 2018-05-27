import { async } from '@angular/core/testing';
import { Brewery } from './../models/brewery';
import { BreweriesService } from './../breweries.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, NgZone, ChangeDetectorRef } from '@angular/core';
import {} from '@types/googlemaps';
import { trigger,state,style,transition,animate,keyframes } from '@angular/animations';

declare const google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  animations: [
    trigger('myAwesomeAnimation', [
        state('small', style({
            transform: 'scale(1)',
        })),
        state('large', style({
            transform: 'scale(1.3) translateX(-100px)',
        })),
        transition('small <=> large', animate('300ms ease-in', keyframes([
          style({opacity: 0, transform: 'translateY(-75%)', offset: 0}),
          style({opacity: 1, transform: 'translateY(35px)',  offset: 0.5}),
          style({opacity: 1, transform: 'translateY(0)',     offset: 1.0})
        ]))),
    ]),
  ]
})
export class MapComponent implements OnInit {

  @Input() brewery: Brewery;
  geocoder: any;
  addressBuilder: string
  lat: number;
  lng: number;
  asyncs: any;
  state: string = 'small';

  constructor(private route: ActivatedRoute, private zone: NgZone,private cdr: ChangeDetectorRef ) { }

  ngOnInit() {
    this.geocoder = new google.maps.Geocoder();
  }

  ngAfterViewInit(){

    this.addressBuilder = this.brewery.Address + "," + this.brewery.ZipCode.toString();
    this.setCoords();
    this.cdr.detectChanges();

  }

  async setCoords(){
    this.asyncs =  await this.geocode(this.addressBuilder);

  }


  geocode(data){
    return this.geocoder.geocode({ 'address': data}, (results, status) => {
        if (status == google.maps.GeocoderStatus.OK)
            this.lat = results[0].geometry.location.lat();
            this.lng = results[0].geometry.location.lng();
            this.zone.run(() => {
              console.log('refreshed google map');
          });
      });

  }

  animateMe() {
    this.state = (this.state === 'small' ? 'large' : 'small');
  }
}
