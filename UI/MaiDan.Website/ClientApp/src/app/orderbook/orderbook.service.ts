import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../shared/app.configuration';
import { Order } from '../shared/models/order';

@Injectable()
export class OrderbookService {

  private url: string;

  constructor(private http: HttpClient, private configuration: Configuration) {
      this.url = configuration.apiUrl + 'orderbook/';
  }

  getOrders(): Observable<Order[]> {
      return this.http.get<Order[]>(this.url);
  }

  getOrder(id: number): Observable<Order> {
      return this.http.get<Order>(this.url + id);
  }

  addOrder(order: Order): Observable<number> {
    return this.http.post<number>(this.url, order);
  }

  updateOrder(order: Order): Observable<Order> {
    return this.http.put<Order>(this.url, order);
  }

}
