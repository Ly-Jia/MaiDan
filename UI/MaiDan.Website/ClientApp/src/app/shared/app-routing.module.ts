import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { OrderbookComponent } from '../orderbook/orderbook.component';
import { OrderComponent } from '../order/order.component';
import { BillbookComponent } from '../billbook/billbook.component';
import { BillComponent } from '../bill/bill.component';
import { MenuComponent } from '../menu/menu.component';
import { DishComponent } from '../dish/dish.component';
import { SlipbookComponent } from '../slipbook/slipbook.component';
import { SlipComponent } from '../slip/slip.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'orderbook', component: OrderbookComponent },
  { path: 'orderbook/:id', component: OrderComponent },
  { path: 'billbook', component: BillbookComponent },
  { path: 'billbook/:id', component: BillComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'menu/:id', component: DishComponent },
  { path: 'slipbook', component: SlipbookComponent },
  { path: 'slipbook/:id', component: SlipComponent }

];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
