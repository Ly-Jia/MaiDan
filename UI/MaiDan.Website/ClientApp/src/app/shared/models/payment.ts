export class Payment {
  id: number;
  paymentMethodId: string;
  paymentMethodLabel: string;
  amount: number;

  constructor(id: number) {
    this.id = id;
  }
}
