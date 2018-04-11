import { Component, OnInit } from '@angular/core';
import { MenuService } from './menu.service';
import { Dish } from '../shared/models/dish';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  dishes: Dish[];
  constructor(private menuService: MenuService) { }

  ngOnInit() {
    this.menuService.getDishes()
      .subscribe({
        next: value => this.dishes = value,
        error: err => { console.log(`Cannot load dishes`); console.log(err); },
        complete: () => console.log(`Dishes loaded`)
      });
  }

}
