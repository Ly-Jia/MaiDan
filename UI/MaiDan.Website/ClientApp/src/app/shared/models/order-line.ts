export class OrderLine {
  id: number;
  quantity: number;
  free: boolean;
  dishId: string;
  dishLabel: string;
  amount: number;

  constructor(id: number) {
    this.id = id;
  }
}
