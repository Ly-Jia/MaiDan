import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from './dashboard.service';
import { DaySlip } from '../shared/models/day-slip';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  day: Date;
  slip: DaySlip;
  constructor(
    private router: Router,
    private dashboardService: DashboardService
  ) { }

  ngOnInit() {
    this.slip = new DaySlip();
    this.dashboardService.currentDay()
      .subscribe({
        next: value => this.day = value,
        error: err => { console.log(`Cannot load current day`); console.log(err); },
        complete: () => { console.log(`Fetched current day: ` + this.day); }
      });
  }

  openDay() {
    this.dashboardService.openDay()
      .subscribe({
        next: () => { },
        error: err => {
          console.log(`Cannot open day`); console.log(err);
          this.router.navigate(['orderbook']); 
        },
        complete: () => { console.log(`Day opened`); }
      });
  }

  closeDay() {
    this.slip.day = this.day;
    this.dashboardService.closeDay(this.slip)
      .subscribe({
        next: () => { },
        error: err => {
          console.log(`Cannot close day`); console.log(err);
          console.log(this.slip.day);
          console.log(this.slip.cashAmount);
        },
        complete: () => {
          console.log(`Day closed`);
          this.slip = new DaySlip();
          this.router.navigate(['dashboard']);
        }
      });
  }
}
