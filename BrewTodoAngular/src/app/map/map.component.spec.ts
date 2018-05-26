import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AgmCoreModule, MapsAPILoader } from '@agm/core';
import { MapComponent } from './map.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('MapComponent', () => {
  let component: MapComponent;
  let fixture: ComponentFixture<MapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AgmCoreModule, RouterTestingModule],
      declarations: [ MapComponent ],
      providers: [MapsAPILoader]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
