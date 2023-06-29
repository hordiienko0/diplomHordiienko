import {Injectable} from '@angular/core';
import {IPhase} from "../models/phase.model";
import {ApiService} from "../../../../core/resources/services/api.service";
import {HttpClient} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {IPhaseStep} from "../models/phase-step.model";

@Injectable({
  providedIn: 'root'
})
export class PhaseApiService extends ApiService{

  apiPath = "/phases";

  constructor(http: HttpClient) {
    super(http);
  }

  getPhasesByProjectId(projectId : number): Observable<IPhase[]> {
    return this.get<IPhase[]>(`${this.apiPath}/project/${projectId}`);
  }

  addPhaseToProject(phase : IPhase, projectId : number) : Observable<void> {
    return this.post<void>(`${this.apiPath}`, {
      projectId: projectId,
      phaseName : phase.phaseName,
      startDate: phase.startDate,
      endDate: phase.endDate,
      phaseSteps: phase.phaseSteps
    });
  }

  editPhase(phase: IPhase) :Observable<void> {
    return this.put<void>(`${this.apiPath}/${phase.id}`, {
      id: phase.id,
      phaseName: phase.phaseName,
      startDate: phase.startDate,
      endDate: phase.endDate
    });
  }

  editPhaseSteps(phaseId: number, phaseSteps : IPhaseStep[]) :Observable<void> {
    return this.put<void>(`${this.apiPath}/steps`, {
      phaseId: phaseId,
      phaseSteps: phaseSteps
    });
  }

  deletePhase(id :number) : Observable<void> {
    return this.delete<void>(`${this.apiPath}/${id}`);
  }

  editPhaseStep(phaseStep : IPhaseStep) : Observable<void> {
    return this.put<void>(`${this.apiPath}/steps/${phaseStep.id}`,{
      id: phaseStep.id,
      isDone: phaseStep.isDone
    })
  }

  mockData: IPhase[] = [
    {
      id: 1,
      phaseName: 'Project initiation',
      startDate: new Date('04/02/2019').toDateString(),
      endDate: new Date('04/21/2019').toDateString(),
      progress: 67,
      phaseSteps: [
        {
          id: 2,
          phaseStepName: 'Identify site location',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: false
        },
        {
          id: 3,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        },
        {
          id: 4,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        }
      ]
    },
    {
      id: 2,
      phaseName: 'Project initiation',
      startDate: new Date('04/02/2019').toDateString(),
      endDate: new Date('04/21/2019').toDateString(),
      progress: 67,
      phaseSteps: [
        {
          id: 5,
          phaseStepName: 'Identify site location',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: false
        },
        {
          id: 6,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        },
        {
          id: 7,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        }
      ]
    },
    {
      id: 3,
      phaseName: 'Project initiation',
      startDate: new Date('04/02/2019').toDateString(),
      endDate: new Date('04/21/2019').toDateString(),
      progress: 67,
      phaseSteps: [
        {
          id: 8,
          phaseStepName: 'Identify site location',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: false
        },
        {
          id: 9,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        },
        {
          id: 10,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        }
      ]
    },
    {
      id: 4,
      phaseName: 'Project initiation',
      startDate: new Date('04/02/2019').toDateString(),
      endDate: new Date('04/21/2019').toDateString(),
      progress: 67,
      phaseSteps: [
        {
          id: 11,
          phaseStepName: 'Identify site location',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: false
        },
        {
          id: 12,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        },
        {
          id: 13,
          phaseStepName: 'Perform Soil test',
          startDate: new Date('04/02/2019').toDateString(),
          endDate: new Date('05/02/2019').toDateString(),
          isDone: true
        }
      ]
    },
  ];


}
