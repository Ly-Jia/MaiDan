import { OrderLine } from "./order-line";

export class Order {
    id: number;
    tableId: string | undefined;
    numberOfGuests: number | undefined;
    total: number;
    lines: OrderLine[];
  }