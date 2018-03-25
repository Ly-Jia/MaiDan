import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderbookComponent } from '../orderbook/orderbook.component';
import { OrderComponent } from '../order/order.component';
import { BillbookComponent } from '../billbook/billbook.component';
import { BillComponent } from '../bill/bill.component';
import { MenuComponent } from '../menu/menu.component';
import { DishComponent } from '../dish/dish.component';

const routes: Routes = [
  { path: 'orderbook', component: OrderbookComponent },
  { path: 'orderbook/:id', component: OrderComponent },
  { path: 'billbook', component: BillbookComponent },
  { path: 'billbook/:id', component: BillComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'menu/:id', component: DishComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
