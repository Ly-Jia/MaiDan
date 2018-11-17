import { Component, OnInit } from '@angular/core';
import { OrderbookService } from './orderbook.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Http, Response } from '@angular/http'
import { BillbookService } from '../billbook/billbook.service';
import { MenuService } from '../menu/menu.service';
import { RoomService } from '../room/room.service';
import { Order } from '../shared/models/order';
import { OrderLine } from '../shared/models/order-line'
import { Dish } from '../shared/models/dish';
import { Table } from '../shared/models/table';
import { SelectItem } from 'primeng/api';
import { DropdownModule } from 'primeng/dropdown';
import { SpinnerModule } from 'primeng/spinner';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FieldsetModule } from 'primeng/fieldset';
import { startWith, map, mergeMap } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-orderbook',
  templateUrl: './orderbook.component.html',
  styleUrls: ['./orderbook.component.css']
})
export class OrderbookComponent implements OnInit {

  newOrder: Order;
  orders: Order[];
  menu: Dish[];
  tables: Table[];
  lineFormControl = new FormControl();
  takeAwayFormControl = new FormControl;
  filteredOptions: Observable<string[]>;
  options: string[];

  constructor(
    private orderbookService: OrderbookService,
    private billbookService: BillbookService,
    private menuService: MenuService,
    private roomService: RoomService) { }

  ngOnInit() {
    this._refreshData();

    this.filteredOptions = this.lineFormControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
    );

    this.hideOnSiteOrderPanel();

  }

  addLine() {
    const line = new OrderLine(this.newOrder.lines.length + 1);
    line.quantity = 1;
    this.newOrder.lines.push(line);
  }
 
  add(): void {
    this.newOrder.lines = this.newOrder.lines.filter(l => l.dishLabel != null);
    this.newOrder.lines.forEach(l => l.dishId = this._getDishIdFromLabel(l.dishLabel));
    this.orderbookService.addOrder(this.newOrder)
      .subscribe({
        next: () => { },
        error: err => { console.log(`Cannot create order`); console.log(err); },
        complete: () => { console.log(`Order created`); this._refreshData(); }
      });
  }

  showOnSiteOrderPanel() {
    document.getElementById("on-site-order").style.display = "";
  }

  hideOnSiteOrderPanel() {
    document.getElementById("on-site-order").style.display = "none";
  }

  private _refreshData() {
    this.newOrder = new Order(false, new Array<OrderLine>(), 0);
    this.addLine();

    this.menuService.getDishes().pipe(
      map(menu => {
        this.menu = menu;
        this.options = menu.map(dish => this._buildDishLabel(dish.id));
        return menu;
      }),
      mergeMap(menu => this.roomService.getTables()),
      map(room => {
        this.tables = room;
      }),
      mergeMap(room => this.orderbookService.getOrders()),
      map(orders => {
        this.orders = orders;
      })
    ).subscribe();
  }

  private _buildDishLabel(dishId: string): string {
    const dish = this.menu.find(d => d.id == dishId);
    return dish.id + " - " + dish.name;
  }

  private _getDishIdFromLabel(dishLabel: string): string {
    var regex = new RegExp("([a-zA-Z0-9]*) - .*");
    var m = regex.exec(dishLabel);
    return m[1];
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
