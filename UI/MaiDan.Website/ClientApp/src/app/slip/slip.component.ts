import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SlipbookService } from '../slipbook/slipbook.service';
import { Slip } from '../shared/models/slip';
import { Payment } from '../shared/models/payment';

@Component({
  selector: 'app-slip',
  templateUrl: './slip.component.html',
  styleUrls: ['./slip.component.css']
})
export class SlipComponent implements OnInit {

  slip: Slip;
  payments: Payment[];
  constructor(
    private route: ActivatedRoute,
    private slipbookService: SlipbookService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.slipbookService.getSlip(id)
      .subscribe({
        next: value => {
          this.slip = value;
          this.payments = this.slip.payments;
          this.payments.push(new Payment(this.payments.length + 1));
        },
        error: err => { console.log(`Cannot load slip ${id}`); console.log(err); },
        complete: () => console.log(`Slip ${id} loaded`)
      });
  }

  addPayment() {
    this.slip.payments.push(new Payment(this.payments.length + 1));
  }

  savePayments() {
    this.slipbookService.updateSlip(this.slip)
      .subscribe({
        next: () => { },
        error: err => { console.log(`Cannot update slip ${this.slip.id}`); console.log(err); },
        complete: () => console.log(`Slip ${this.slip.id} updated`)
      });
  }

}
