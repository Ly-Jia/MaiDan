import { Component, OnInit } from '@angular/core';
import { BillbookService } from './billbook.service';
import { Order } from '../shared/models/order';

@Component({
  selector: 'app-billbook',
  templateUrl: './billbook.component.html',
  styleUrls: ['./billbook.component.css']
})
export class BillbookComponent implements OnInit {

  bills: Order[];
  constructor(private billbookService: BillbookService) { }

  ngOnInit() {
    this.billbookService.getBills()
      .subscribe({
        next: value => this.bills = value,
        error: err => { console.log(`Cannot load bills`); console.log(err); },
        complete: () => console.log(`Bills loaded`)
      });
  }

}
