import { OrderLine } from "./order-line";
import { Table } from "./table";

export class Order {
    id: number;
    isTakeAway: boolean;
    table: Table;
    numberOfGuests: number | undefined;
    total: number;
    lines: OrderLine[];

    constructor(isTakeAway: boolean, lines: OrderLine[], numberOfGuests: number | undefined) {
      this.id = 0;
      this.isTakeAway = isTakeAway;
      this.total = 0;
      this.lines = lines;
      this.numberOfGuests = numberOfGuests;
    }
  }
