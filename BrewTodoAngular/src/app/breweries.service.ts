import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BreweriesService {

  constructor(private httpClient: HttpClient) { }

  getBreweries(onSuccess){
    let url = "http://ec2-18-222-156-248.us-east-2.compute.amazonaws.com/BrewTodoServer_deploy/api/breweries";
    let request = this.httpClient.get(url);
    let promise = request.toPromise();

    promise.then(
      onSuccess,
      (reason) => console.log(reason)
    )
  }

  getBreweriesByID(id: number){

  }

}
