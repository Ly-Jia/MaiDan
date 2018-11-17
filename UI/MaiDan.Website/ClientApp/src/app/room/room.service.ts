import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../shared/app.configuration';
import { Table } from '../shared/models/table';

@Injectable()
export class RoomService {

  private url: string;

  constructor(private http: HttpClient, private configuration: Configuration) {
    this.url = configuration.apiUrl + 'room/';
  }

  getTables(): Observable<Table[]> {
    return this.http.get<Table[]>(this.url);
  }
}
