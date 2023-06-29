import { Order } from "src/app/modules/project/resources/models/order";

export interface Params {
  page: number,
  count: number,
  query: string,
  sort: string,
  order: Order,
}