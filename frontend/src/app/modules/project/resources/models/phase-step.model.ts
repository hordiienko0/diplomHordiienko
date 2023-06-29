export interface IPhaseStep {
  id: number,
  phaseStepName: string,
  isDone : boolean,
  startDate : string,
  endDate: string,
  buildingId? : number
}
