import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../shared/app.configuration';
import { DaySlip } from "../shared/models/day-slip";
import { Day } from "../shared/models/day";

@Injectable()
export class DashboardService {

  private url: string;

  constructor(private http: HttpClient, private configuration: Configuration) {
      this.url = configuration.apiUrl + 'calendar/';
  }

  currentDay(): Observable<Date> {
    return this.http.get<Date>(this.url);
  }
  
  openDay(): Observable<number> {
    return this.http.post<number>(this.url, {});
  }

  closeDay(daySlip: DaySlip): Observable<number> {
    return this.http.put<number>(this.url, daySlip);
  }
}
