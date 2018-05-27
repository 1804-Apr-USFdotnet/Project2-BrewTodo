import { Brewery } from './../models/brewery';
import { Component, OnInit, Input,ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {

  @Input() brewery: Brewery;
  reviews;

  constructor(private cdr: ChangeDetectorRef) {

   }

  ngOnInit() {

  }
  ngAfterViewInit(){
    this.reviews = this.brewery.Reviews;
    this.cdr.detectChanges();

  }

}
