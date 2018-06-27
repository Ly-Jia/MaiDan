import { Component, OnInit } from '@angular/core';
import { SlipbookService } from './slipbook.service';
import { Slip } from '../shared/models/slip';

@Component({
  selector: 'app-slipbook',
  templateUrl: './slipbook.component.html',
  styleUrls: ['./slipbook.component.css']
})
export class SlipbookComponent implements OnInit {

  slips: Slip[];
  constructor(private slipbookService: SlipbookService) { }

  ngOnInit() {
    this.slipbookService.getSlips()
      .subscribe({
        next: value => this.slips = value,
        error: err => { console.log(`Cannot load slips`); console.log(err); },
        complete: () => console.log(`Slips loaded`)
      });
  }

}
