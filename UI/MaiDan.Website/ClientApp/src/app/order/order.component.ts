import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderbookService } from '../orderbook/orderbook.service';
import { BillbookService } from '../billbook/billbook.service';
import { Order } from '../shared/models/order';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  order: Order;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderbookService: OrderbookService,
    private billbookService: BillbookService) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.orderbookService.getOrder(id)
      .subscribe({
        next: value => this.order = value,
        error: err => console.log(`Cannot load order ${id}. ${err}`),
        complete: () => console.log(`Order ${id} loaded`)
      });
  }

  print(id: number): void {
    this.billbookService.printBill(id)
      .subscribe({
        next: () => {},
        error: err => console.log(`Cannot create bill ${id}. ${err}`),
        complete: () => {
          console.log(`Bill ${id} created`);
          this.router.navigate(['billbook', id]);
        }
      });
  }

}
