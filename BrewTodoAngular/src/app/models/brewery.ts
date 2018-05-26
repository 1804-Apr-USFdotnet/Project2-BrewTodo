import { State } from './state';
import { Reviews } from './reviews';
import { Beers } from './beers';

export class Brewery {
    Address: string;
    AverageRating: number;
    Beers: Beers[];
    BreweryID: number;
    BusinessHours: string;
    Description: string;
    HasFood: boolean;
    HasGrowler: boolean;
    HasMug: boolean;
    HasTShirt: boolean;
    ImageURL: string;
    Name: string;
    PhoneNumber: string;
    Review: Reviews[];
    State: State;
    StateID: number
    ZipCode: string;

}
