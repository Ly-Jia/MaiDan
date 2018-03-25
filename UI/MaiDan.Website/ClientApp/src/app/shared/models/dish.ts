import { DishPriceConfiguration } from "./dish-price-configuration";

export class Dish {
    id: string;
    name: string;
    price: number;
    priceConfiguration: DishPriceConfiguration[];
  }