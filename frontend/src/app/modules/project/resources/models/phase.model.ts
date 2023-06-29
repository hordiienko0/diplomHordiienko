import {IPhaseStep} from "./phase-step.model";

export interface IPhase {
  id: number,
  phaseName : string,
  progress : number,
  startDate : string,
  endDate : string,
  phaseSteps : IPhaseStep[]
}
