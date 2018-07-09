export class Payment {
  id: number;
  paymentMethodId: string;
  amount: number;

  constructor(id: number) {
    this.id = id;
  }
}
