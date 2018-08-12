import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Http, Response } from '@angular/http'
import { SlipbookService } from '../slipbook/slipbook.service';
import { PaymentMethodListService } from '../payment-method-list/payment-method-list.service';
import { Slip } from '../shared/models/slip';
import { Payment } from '../shared/models/payment';
import { PaymentMethod } from '../shared/models/payment-method';
import { SelectItem } from 'primeng/api';
import { DropdownModule } from 'primeng/dropdown';
import { startWith, map, mergeMap } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-slip',
  templateUrl: './slip.component.html',
  styleUrls: ['./slip.component.css']
})
export class SlipComponent implements OnInit {

  slip: Slip;
  paymentMethods: PaymentMethod[];
  myControl = new FormControl();
  filteredOptions: Observable<string[]>;
  options: string[];

  constructor(
    private route: ActivatedRoute,
    private slipbookService: SlipbookService,
    private paymentMethodListService: PaymentMethodListService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.paymentMethodListService.getPaymentMethods().pipe(
      map(pm => {
        this.paymentMethods = pm;
        this.options = this.paymentMethods.map(p => this._buildPaymentMethodLabel(p.id));
        return pm;
      }),
      mergeMap(pm => this.slipbookService.getSlip(id)),
      map(slip => {
        this.slip = slip;
        this.slip.payments.forEach(p => p.paymentMethodLabel = this._buildPaymentMethodLabel(p.paymentMethodId));
        this.slip.payments.push(new Payment(this.slip.payments.length + 1));
      })
    ).subscribe();
     
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  addPayment() {
    this.slip.payments.push(new Payment(this.slip.payments.length + 1));
  }

  savePayments() {
    this.slip.payments = this.slip.payments.filter(p => p.amount != null);
    this.slip.payments.forEach(p => p.paymentMethodId = this._getPaymentMethodIdFromLabel(p.paymentMethodLabel));
    this.slipbookService.updateSlip(this.slip)
      .subscribe({
        next: () => { },
        error: err => { console.log(`Cannot update slip ${this.slip.id}`); console.log(err); },
        complete: () => console.log(`Slip ${this.slip.id} updated`)
      });
  }

  private _buildPaymentMethodLabel(paymentMethodId: string): string {
    const paymentMethod = this.paymentMethods.find(pm => pm.id == paymentMethodId);
    return paymentMethod.id + " - " + paymentMethod.name;
  }

  private _getPaymentMethodIdFromLabel(paymentMethodLabel: string): string {
    var regex = new RegExp("([a-zA-Z0-9]*) - .*");
    var m = regex.exec(paymentMethodLabel);
    return m[1];
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
