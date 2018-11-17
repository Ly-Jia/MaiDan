import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Http, Response } from '@angular/http'
import { OrderbookService } from '../orderbook/orderbook.service';
import { BillbookService } from '../billbook/billbook.service';
import { MenuService } from '../menu/menu.service';
import { Order } from '../shared/models/order';
import { OrderLine } from '../shared/models/order-line';
import { Dish } from '../shared/models/dish';
import { SelectItem } from 'primeng/api';
import { SpinnerModule } from 'primeng/spinner';
import { startWith, map, mergeMap } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  order: Order;
  menu: Dish[];
  myControl = new FormControl();
  filteredOptions: Observable<string[]>;
  options: string[];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderbookService: OrderbookService,
    private billbookService: BillbookService,
    private menuService: MenuService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.menuService.getDishes().pipe(
      map(menu => {
        this.menu = menu;
        this.options = menu.map(dish => this._buildDishLabel(dish.id));
        return menu;
      }),
      mergeMap(menu => this.orderbookService.getOrder(id)),
      map(order => {
        this.order = order;
        this.order.lines.forEach(l => l.dishLabel = this._buildDishLabel(l.dishId));
        this.order.lines.push(new OrderLine(this.order.lines.length + 1));
      })
    ).subscribe();

    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  addLine() {
    this.order.lines.push(new OrderLine(this.order.lines.length + 1));
  }

  update(order: Order): void {
    this.order.lines = this.order.lines.filter(l => l.quantity != null);
    this.order.lines.forEach(l => l.dishId = this._getDishIdFromLabel(l.dishLabel));
    this.orderbookService.updateOrder(order)
      .subscribe({
        next: () => { },
        error: err => { console.log(`Cannot update order ${order.id}`); console.log(err); },
        complete: () => console.log(`Order ${order.id} updated`)
      });
  }

  trackByIndex(index: number, item: Order): number {
    return index;
  }

  print(id: number): void {
    this.billbookService.printBill(id)
      .subscribe({
        next: () => { },
        error: err => { console.log(`Cannot create bill ${id}`); console.log(err); },
        complete: () => {
          console.log(`Bill ${id} created`);
          this.router.navigate(['billbook', id]);
        }
      });
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
