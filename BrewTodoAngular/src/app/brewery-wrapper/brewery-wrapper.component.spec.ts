import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BreweryWrapperComponent } from './brewery-wrapper.component';

describe('BreweryWrapperComponent', () => {
  let component: BreweryWrapperComponent;
  let fixture: ComponentFixture<BreweryWrapperComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BreweryWrapperComponent ]
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
