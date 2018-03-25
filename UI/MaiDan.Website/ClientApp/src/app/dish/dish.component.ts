import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuService } from '../menu/menu.service';
import { Dish } from '../shared/models/dish';

@Component({
  selector: 'app-dish',
  templateUrl: './dish.component.html',
  styleUrls: ['./dish.component.css']
})
export class DishComponent implements OnInit {

  dish: Dish;
  constructor(
    private route: ActivatedRoute,
    private menuService: MenuService) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.menuService.getDish(id)
      .subscribe({
        next: value => this.dish = value,
        error: err => console.log(`Cannot load dish ${id}. ${err}`),
        complete: () => console.log(`Dish ${id} loaded`)
      });
  }

}
