import { async } from '@angular/core/testing';
import { Brewery } from './../models/brewery';
import { BreweriesService } from './../breweries.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, NgZone } from '@angular/core';
import {} from '@types/googlemaps';
declare const google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  @Input() brewery: Brewery;
  geocoder: any;
  addressBuilder: string
  lat: number;
  lng: number;
  asyncs: any;
  geoData: any;
  promise: any;

  constructor(private route: ActivatedRoute, private zone: NgZone ) { }

  ngOnInit() {
    this.geocoder = new google.maps.Geocoder();
  }

  ngAfterViewInit(){
    this.addressBuilder = this.brewery.Address + "," + this.brewery.ZipCode.toString();
    this.setCoords();
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


}
