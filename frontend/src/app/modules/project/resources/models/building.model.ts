import {IBuildingBlock} from "./building-block.model";

export interface IBuilding {
  id: number,
  buildingName: string,
  buildingProgress: number,
  buildingBlocks: IBuildingBlock[],
  projectId: number
}
