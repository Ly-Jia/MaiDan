import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { AppRoutingModule } from './shared/app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { Configuration } from './shared/app.configuration';
import { OrderbookService } from './orderbook/orderbook.service';
import { BillbookService } from './billbook/billbook.service';
import { MenuService } from './menu/menu.service';
import { OrderbookComponent } from './orderbook/orderbook.component';
import { BillbookComponent } from './billbook/billbook.component';
import { DishComponent } from './dish/dish.component';
import { OrderComponent } from './order/order.component';
import { BillComponent } from './bill/bill.component';

@NgModule({
  declarations: [
    AppComponent,
    OrderbookComponent,
    BillbookComponent,
    MenuComponent,
    DishComponent,
    OrderComponent,
    BillComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [Configuration, OrderbookService, BillbookService, MenuService],
  bootstrap: [AppComponent]
})
export class AppModule { }
