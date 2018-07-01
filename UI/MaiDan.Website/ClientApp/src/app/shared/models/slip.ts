import { Order } from "./order";
import { Payment } from "./payment";

export class Slip extends Order {
  paymentDate: Date;
  payments: Payment[];
}
