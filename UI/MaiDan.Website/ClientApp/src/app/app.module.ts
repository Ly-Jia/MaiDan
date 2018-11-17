import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { SpinnerModule } from 'primeng/spinner';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FieldsetModule } from 'primeng/fieldset';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { AppRoutingModule } from './shared/app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { Configuration } from './shared/app.configuration';
import { OrderbookService } from './orderbook/orderbook.service';
import { BillbookService } from './billbook/billbook.service';
import { MenuService } from './menu/menu.service';
import { RoomService } from './room/room.service';
import { SlipbookService } from './slipbook/slipbook.service';
import { PaymentMethodListService } from './payment-method-list/payment-method-list.service';
import { OrderbookComponent } from './orderbook/orderbook.component';
import { BillbookComponent } from './billbook/billbook.component';
import { SlipbookComponent } from './slipbook/slipbook.component';
import { DishComponent } from './dish/dish.component';
import { OrderComponent } from './order/order.component';
import { BillComponent } from './bill/bill.component';
import { SlipComponent } from './slip/slip.component';

import { MatAutocompleteModule, MatFormFieldModule, MatInputModule } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    OrderbookComponent,
    BillbookComponent,
    SlipbookComponent,
    MenuComponent,
    DishComponent,
    OrderComponent,
    BillComponent,
    SlipComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    TableModule,
    DropdownModule,
    SpinnerModule,
    RadioButtonModule,
    FieldsetModule,
    BrowserAnimationsModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  providers: [Configuration, OrderbookService, BillbookService, SlipbookService, MenuService, PaymentMethodListService, RoomService],
  bootstrap: [AppComponent]
})
export class AppModule { }
