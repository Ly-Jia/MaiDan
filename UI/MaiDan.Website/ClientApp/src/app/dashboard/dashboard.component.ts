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
  resultMessage = '';
  isError = false;
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
    this.resultMessage = '';
    this.isError = false;
    this.dashboardService.openDay()
      .subscribe({
        next: () => { },
        error: err => {
          this.resultMessage = `Impossible d'ouvrir la journée. ${this.getOpenDayErrorMessage(err.error)}.`;
          this.isError = true;
          this.router.navigate(['orderbook']);
        },
        complete: () => {
          this.resultMessage = 'Jour ouvert.';
          this.isError = false;
        }
      });
  }

  closeDay() {
    this.resultMessage = '';
    this.isError = false;
    this.slip.day = this.day;
    this.dashboardService.closeDay(this.slip)
      .subscribe({
        next: () => { },
        error: err => {
          this.resultMessage = `Impossible de fermer la journée. ${this.getCloseDayErrorMessage(err.error)}.`;
          this.isError = true;
        },
        complete: () => {
          this.resultMessage = 'Jour fermé.';
          this.isError = false;
        }
      });
  }

  getOpenDayErrorMessage(code: string): string {
    switch (code) {
      case 'TodayAlreadyOpened':
        return 'Jour d\'aujourd\'hui déjà ouvert';
      case 'AnotherDayAlreadyOpened':
        return 'Un autre jour est déjà ouvert';
      default:
        return 'Erreur inattendue';
    }
  }

  getCloseDayErrorMessage(code: string): string {
    switch (code) {
      case 'OpenedOrdersPending':
        return 'Des commandes sont en cours';
      case 'OpenedBillsPending':
        return 'Des additions sont en cours';
      case 'DayNotOpened':
        return 'Jour non ouvert';
      case 'DayAlreadyClosed':
        return 'Jour déjà fermé';
      default:
        return 'Erreur inattendue';
    }
  }
}
