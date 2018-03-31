import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BillbookService } from '../billbook/billbook.service';
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
    private billbookService: BillbookService) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.billbookService.getBill(id)
      .subscribe({
        next: value => this.bill = value,
        error: err => { console.log(`Cannot load bill ${id}.`); console.log(err); },
        complete: () => console.log(`Bill ${id} loaded`)
      });
  }

}
