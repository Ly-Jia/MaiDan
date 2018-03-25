import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Configuration } from '../shared/app.configuration';
import { Order } from '../shared/models/order';

@Injectable()
export class BillbookService {

  private url: string;

  constructor(private http: HttpClient, private configuration: Configuration) {
      this.url = configuration.apiUrl + 'billbook/';
  }

  getBills(): Observable<Order[]> {
      return this.http.get<Order[]>(this.url);
  }

  getBill(id: string): Observable<Order> {
      return this.http.get<Order>(this.url + id);
  }

  printBill(orderId: number): Observable<Order> {
    const order = new Order();
    order.id = orderId;
    return this.http.post<Order>(this.url, order);
  }
}
