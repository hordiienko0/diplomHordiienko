import {Component, EventEmitter, Input, OnChanges, OnInit, Output} from '@angular/core';
import {GridLine, TaskFieldsModel} from "@syncfusion/ej2-angular-gantt";
import {PhaseApiService} from "../resources/services/phase-api.service";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {map} from "rxjs/operators";
import {selectProjectPhases} from "../state/project.selectors";
import {Observable} from "rxjs";
import {IPhase} from "../resources/models/phase.model";
import {IProjectDetailed} from "../resources/models/project-details";
import * as moment from 'moment';
import {updatePhaseStep} from "../state/project.actions";
import {IPhaseStep} from "../resources/models/phase-step.model";

@Component({
  selector: 'app-gantt-chart',
  templateUrl: './gantt-chart.component.html',
  styleUrls: ['./gantt-chart.component.scss']
})
export class GanttChartComponent implements OnInit, OnChanges {

  constructor(private store: Store<AppState>) {
  }

  public data?: object[];
  public resources?: object[];
  public taskSettings?: object;
  public timelineSettings?: object;
  public gridLines?: GridLine;
  public projectStartDate?: Date;
  public projectEndDate?: Date;
  public splitterSettings?: object;

  @Input() phases? : IPhase[]
  @Input() project? : IProjectDetailed | null
  ngOnInit() {

    this.taskSettings = {
      id: 'TaskID',
      name: 'TaskName',
      startDate: 'StartDate',
      endDate: 'EndDate',
      duration: 'Duration',
      progress: 'Progress',
      child: 'subtasks',
    };

    this.timelineSettings = {
      timelineUnitSize: 300,
      topTier: {
        unit: 'Month'
      },
      bottomTier: {
        unit: 'Week',
        count: 1
      }
    };
    this.gridLines = 'Both';

    this.splitterSettings = {
      columnIndex: 2
    };
  }


  @Input() mode = "Mouth";
  ngOnChanges() {
    this.data = this.phases?.map((phase) => ({
      TaskID: phase.id,
      TaskName: phase.phaseName,
      StartDate: new Date(phase.startDate),
      EndDate: new Date(phase.endDate),
      Progress: phase.progress,
      subtasks: phase.phaseSteps.map(phaseStep => ({
        TaskID: phaseStep.id,
        TaskName: phaseStep.phaseStepName,
        StartDate: new Date(phaseStep.startDate).toDateString(),
        EndDate: new Date(phaseStep.endDate).toDateString()
      }))
    }));

    this.projectStartDate = moment(this.project?.startTime).toDate();
    this.projectEndDate = moment(this.project?.endTime).toDate();

    if (this.mode == "Week") {
      this.timelineSettings = {
        timelineUnitSize: 30,
        topTier: {
          unit: 'Week',
          format: 'dd, MMM, y',
        },
        bottomTier: {
          unit: 'Day',
          count: 1
        },
      };
      return;
    }

    if (this.mode == "Month") {
      this.timelineSettings = {
        timelineUnitSize: 300,
        topTier: {
          unit: 'Month'
        },
        bottomTier: {
          unit: 'Week',
          count: 1
        }
      }
      return;
    }

    if (this.mode == "Quarter") {
      this.timelineSettings = {
        timelineUnitSize: 500,
        topTier: {
          unit: 'None',
        },
        bottomTier: {
          unit: 'Month',
          count: 4
        }
      }
    }
  }

  onCheckboxChanged(checked: boolean, TaskID: any) {
    let result = {} as IPhaseStep;
    this.phases?.forEach(x =>{
      x.phaseSteps.forEach(y => {
        if (y.id == TaskID){
          result = y;
          return;
        }
      })
    });
    if (result == null) {
      return;
    }
    result = {...result, isDone:checked};
    this.store.dispatch(updatePhaseStep({phaseStep: result}))
  }

  @Output() editRequested = new EventEmitter<IPhase>()
  phaseEditRequested(id :any){
    this.editRequested.emit(this.phases?.find(phase => phase.id == id));
  }

  getChecked(taskId: any) : boolean {
    let result = false;
    this.phases?.forEach(x =>{
      x.phaseSteps.forEach(y => {
        if (y.id == taskId){
          result = y.isDone;
          return;
        }
      })
    });
    return result;
  }

  getProgress(taskId: any) {
    return this.phases?.find(x => x.id == taskId)?.progress;
  }
}
