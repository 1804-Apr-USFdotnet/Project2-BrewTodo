import { TestBed, inject } from '@angular/core/testing';

import { BreweriesService } from './breweries.service';

describe('BreweriesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BreweriesService]
    });
  });

  it('should be created', inject([BreweriesService], (service: BreweriesService) => {
    expect(service).toBeTruthy();
  }));
});
