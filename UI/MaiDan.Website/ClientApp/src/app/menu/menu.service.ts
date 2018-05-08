import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../shared/app.configuration';
import { Dish } from '../shared/models/dish';

@Injectable()
export class MenuService {
    
    private url: string;

    constructor(private http: HttpClient, private configuration: Configuration) {
        this.url = configuration.apiUrl + 'menu/';
    }

    getDishes(): Observable<Dish[]> {
        return this.http.get<Dish[]>(this.url);
    }

    getDish(id: string): Observable<Dish> {
        return this.http.get<Dish>(this.url + id);
    }
}