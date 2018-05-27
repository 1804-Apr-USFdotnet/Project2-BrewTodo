import { TestBed, inject } from '@angular/core/testing';
import {RouterTestingModule} from "@angular/router/testing";
import { HttpClient, HttpHandler } from '@angular/common/http';

import { BreweriesService } from './breweries.service';

describe('BreweriesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
      ],
      providers: [BreweriesService, HttpClient, HttpHandler]
    });
  });

  it('should be created', inject([BreweriesService], (service: BreweriesService) => {
    expect(service).toBeTruthy();
  }));
});
