import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../shared/app.configuration';
import { Bill } from '../shared/models/bill';
import { Slip } from '../shared/models/slip';

@Injectable()
export class SlipbookService {

  private url: string;

  constructor(private http: HttpClient, private configuration: Configuration) {
      this.url = configuration.apiUrl + 'slipbook/';
  }
  
  getSlip(id: number): Observable<Slip> {
      return this.http.get<Slip>(this.url + id);
  }

  getSlips(): Observable<Slip[]> {
    return this.http.get<Slip[]>(this.url);
  }

  payBill(billId: number): Observable<number> {
    const bill = new Bill();
    bill.id = billId;
    return this.http.post<number>(this.url, bill);
  }

  updateSlip(slip: Slip): Observable<Slip> {
    slip.payments = slip.payments.filter(p => p.amount != null);
    return this.http.put<Slip>(this.url, slip);
  }

}
