import { Component, OnInit } from '@angular/core';
import { OrderbookService } from './orderbook.service';
import { Order } from '../shared/models/order';

@Component({
  selector: 'app-orderbook',
  templateUrl: './orderbook.component.html',
  styleUrls: ['./orderbook.component.css']
})
export class OrderbookComponent implements OnInit {

  orders: Order[];
  constructor(private orderbookService: OrderbookService) { }

  ngOnInit() {
    this.orderbookService.getOrders()
      .subscribe({
        next: value => this.orders = value,
        error: err => { console.log(`Cannot load orders`); console.log(err); },
        complete: () => console.log(`Orders loaded`)
      });
  }

}
