import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BillbookService } from '../billbook/billbook.service';
import { SlipbookService } from '../slipbook/slipbook.service';
import { Order } from '../shared/models/order';

@Component({
  selector: 'app-bill',
  templateUrl: './bill.component.html',
  styleUrls: ['./bill.component.css']
})
export class BillComponent implements OnInit {

  bill: Order;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private billbookService: BillbookService,
    private slipbookService: SlipbookService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.billbookService.getBill(id)
      .subscribe({
        next: value => this.bill = value,
        error: err => { console.log(`Cannot load bill ${id}`); console.log(err); },
        complete: () => console.log(`Bill ${id} loaded`)
      });
  }

  pay(id: number): void {
    this.slipbookService.payBill(id)
      .subscribe({
        next: () => { },
        error: err => { console.log(`Cannot create slip ${id}`); console.log(err); },
        complete: () => {
          console.log(`Slip ${id} created`);
          this.router.navigate(['slipbook', id]);
        }
      });
  }
}
