import { IPhaseOverviewDTO } from "./phase-overview";
import { ProjectStatus } from "./status";

export interface IProjectOverview {
  id: number,
  projectName: string,
  projectType: string,
  country: string,
  address: string,
  city: string,
  startTime: string,
  endTime: string,
  phases: IPhaseOverviewDTO[],
  imageUrl: string | null,
  status: ProjectStatus,
}
