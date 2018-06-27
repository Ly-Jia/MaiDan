import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../shared/app.configuration';
import { PaymentMethod } from '../shared/models/payment-method';

@Injectable()
export class PaymentMethodListService {
  private url: string;

  constructor(private http: HttpClient, private configuration: Configuration) {
    this.url = configuration.apiUrl + 'paymentmethods/';
  }

  getPaymentMethods(): Observable<PaymentMethod[]> {
    return this.http.get<PaymentMethod[]>(this.url);
  }
}
