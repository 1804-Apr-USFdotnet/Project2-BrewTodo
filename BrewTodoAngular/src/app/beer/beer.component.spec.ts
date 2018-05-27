import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeerComponent } from './beer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('BeerComponent', () => {
  let component: BeerComponent;
  let fixture: ComponentFixture<BeerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
      ],
      declarations: [ BeerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should render the logo', async(() => {
    const fixture = TestBed.createComponent(BeerComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('th>abbr').title).toContain('Beer');
  }));
});
