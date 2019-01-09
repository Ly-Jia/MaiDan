import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { SpinnerModule } from 'primeng/spinner';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { AppRoutingModule } from './shared/app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { Configuration } from './shared/app.configuration';
import { DashboardService } from './dashboard/dashboard.service';
import { OrderbookService } from './orderbook/orderbook.service';
import { BillbookService } from './billbook/billbook.service';
import { MenuService } from './menu/menu.service';
import { RoomService } from './room/room.service';
import { SlipbookService } from './slipbook/slipbook.service';
import { PaymentMethodListService } from './payment-method-list/payment-method-list.service';
import { DashboardComponent } from './dashboard/dashboard.component';
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
    DashboardComponent,
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
    CheckboxModule,
    RadioButtonModule,
    FieldsetModule,
    BrowserAnimationsModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    InputTextModule
  ],
  providers: [Configuration, DashboardService, OrderbookService, BillbookService, SlipbookService, MenuService, PaymentMethodListService, RoomService],
  bootstrap: [AppComponent]
})
export class AppModule { }
