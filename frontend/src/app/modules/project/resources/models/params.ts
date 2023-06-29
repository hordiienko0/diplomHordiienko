import { Order } from "./order";
import { ProjectStatus } from "./status";

export interface Params {
  page: number,
  count: number,
  query: string,
  sort: string,
  order: Order,
  status: ProjectStatus
}