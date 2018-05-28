import { HttpClient, HttpHandler } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { ReviewComponent } from './../review/review.component';
import { BeerComponent } from './../beer/beer.component';
import { MapComponent } from './../map/map.component';
import { BreweryComponent } from './../brewery/brewery.component';
import { AgmCoreModule, MapsAPILoader } from '@agm/core';
import { RouterTestingModule } from '@angular/router/testing';

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BreweryWrapperComponent } from './brewery-wrapper.component';

describe('BreweryWrapperComponent', () => {
  let component: BreweryWrapperComponent;
  let fixture: ComponentFixture<BreweryWrapperComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AgmCoreModule, RouterTestingModule],
      declarations: [
        BreweryWrapperComponent,
        BreweryComponent,
        BeerComponent,
        ReviewComponent,
        MapComponent
      ],
      providers: [HttpClient, HttpHandler, MapsAPILoader ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BreweryWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
