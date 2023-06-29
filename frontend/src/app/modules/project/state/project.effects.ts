import {Injectable} from '@angular/core';
import {act, Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, concatMap, withLatestFrom, mergeMap, switchMap} from 'rxjs/operators';
import {Observable, EMPTY, of, combineLatest} from 'rxjs';
import * as ProjectActions from './project.actions';
import { selectCurrentProjectId, selectParams, selectProjectInformation } from './project.selectors';
import { serializeError } from 'serialize-error';
import { IProjectUpdate } from '../resources/models/project-update';
import * as ModalDialogAction from '../../../store/actions/modal-dialog.action';
import { ProjectService } from '../resources/services/project.services';
import { ErrorService } from '../../error/resources/services/error.services';
import { AppState } from 'src/app/store';
import { select, Store } from '@ngrx/store';
import * as fromProjectSelectors from './project.selectors';
import { BuildingApiService } from "../resources/services/building-api.service";
import { IBuilding } from "../resources/models/building.model";
import { ProjectFileService } from "../resources/services/project-file.services";
import * as fromProjectActions from "./project.actions";
import { ResourceApiService } from '../resources/services/resource-api.service';
import {PhaseApiService} from "../resources/services/phase-api.service";


@Injectable()
export class ProjectEffects {

  getProjectsWithParams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.getProjectsWithParams),
      withLatestFrom(this.store.select(fromProjectSelectors.selectParams)),
      concatMap(([_, action]) =>
        this.projectService.getProjectsWithParams(action).pipe(
          map((data) =>
            ProjectActions.getProjectssWithParamsSuccess({
              data,
            })
          ),
          catchError((error) =>
            of(
              ProjectActions.getProjectsWithParamsFailure({
                error: error.error,
              })
            )
          )
        )
      )
    );
  });

  createProject$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.createProject),
      concatMap((action) =>
        this.projectService
          .createProject(action.project)
          .pipe(
            map((data) => ProjectActions.createProjectSuccess()),
            catchError((error) =>
              of(ProjectActions.createProjectFailure({ error: error.error, }))
            )
          )
      )
    );
  });

  createProjectSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.createProjectSuccess),
      map(() => {
        this.store.dispatch(ProjectActions.getProjectsWithParams());
        return ModalDialogAction.closeModalDialog();
      }),
    );
  });

  getProject$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.getDetailedProject),
      concatMap((action) =>
        this.projectService
          .getDetailedProject(action.id)
          .pipe(
            map((data) =>
              ProjectActions.getDetailedProjectSuccess({
                data
              })
            ),
            catchError((error) =>
              of(
                ProjectActions.getDetailedProjectFailure({
                  error: error.error,
                })
              )
            )
          )
      )
    );
  });


  submitProjectInformationFormState$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.submitProjectInformationForm),
      switchMap((project) => {
        return this.projectService
          .putDetailedProject({
            id: project.id,
            address: project.address,
            startTime: project.startTime,
            endTime: project.endTime
          } as IProjectUpdate)
          .pipe(
            map((data) => ProjectActions.getDetailedProject({id: data.id})),
            catchError((error) =>
              of(
                ProjectActions.submitProjectInformationFormFailure({
                  error: serializeError(error),
                })
              )
            )
          );
      })
    );
  });

  loadProjectPhotos$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.loadProjectPhotos),
      mergeMap((action) =>
        this.projectService.getProjectPhotos(action.projectId).pipe(
          map((result) =>
            ProjectActions.loadProjectPhotosSuccess({
              data: result,
            })
          ),
          catchError((error) =>
            of(
              ProjectActions.loadProjectPhotoFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    );
  });

  deleteProjectPhoto$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.deleteProjectPhoto),
      mergeMap((action) =>
        this.projectService
          .deleteProjectPhoto(action.projectId, action.photoId)
          .pipe(
            map((serviceResult) =>
              ProjectActions.deleteProjectPhotoSuccess({
                projectId: serviceResult.projectId,
                id: serviceResult.id,
              })
            ),
            catchError((error) =>
              of(
                ProjectActions.loadProjectPhotoFailure({
                  error: serializeError(error),
                })
              )
            )
          )
      )
    );
  });

  uploadProjectPhotoSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.uploadProjecPhotoSuccess),
      map((action) => ProjectActions.loadProjectPhotos({ projectId: action.id })),
    );
  });

  changeParams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.changeParams),
      map(() => ProjectActions.getProjectsWithParams())
    );
  });

  changeProjectStatus$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.changeProjectStatus),
      concatMap((action) =>
        this.projectService.changeStatus(action.projectId, action.newStatus).pipe(
          map(() => ProjectActions.changeProjectStatusSuccess({ newStatus: action.newStatus })),
          catchError((error: any) =>
            of(ProjectActions.changeProjectStatusFailure({ error }))
          )
        )
      )
    );
  });

  getProjectSuccess = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.getDetailedProjectSuccess,
        ProjectActions.submitProjectInformationFormFailure,
        ProjectActions.cancelEditProjectInformationForm),
      map(() => ProjectActions.loadDisabledProjectInformationForm())
    );
  });

  initiallyLoadBuildings$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.getDetailedProjectSuccess),
      map((action) => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: action.data.id }))
    ))

  loadBuildings$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.loadBuildingWithBuildingBlocks),
      concatMap((action) =>
        this.buildingService.getBuildingsByProjectId(action.projectId).pipe(
          map(result => ProjectActions.loadBuildingWithBuildingBlocksSuccess({ result: result })),
          catchError(error => of(ProjectActions.loadBuildingWithBuildingBlocksFailure({ error: serializeError(error) })))
        ))
    ))

  addNewBuilding$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.addNewBuilding),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, id]) =>
        this.buildingService.addBuilding({ buildingName: action.buildingName, projectId: id } as IBuilding)
          .pipe(
            map(() => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: id! })),
            catchError((error) => of(ProjectActions.addNewBuildingFailure({ error: serializeError(error) })))
          ))
    ));

  updateBuilding$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.updateBuilding),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, id]) =>
        this.buildingService.updateBuilding(action.building).pipe(
          map(() => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: id! })),
          catchError((error) => of(ProjectActions.updateBuildingFailure({ error: serializeError(error) })))
        )
      )
    )
  );

  deleteBuilding$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.deleteBuilding),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, projectId]) =>
        this.buildingService.deleteBuilding(action.id).pipe(
          map(() => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: projectId! })),
          catchError((error) => of(ProjectActions.deleteBuildingFailure({ error: serializeError(error) })))
        )
      )
    )
  );

  addBuildingBlock$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.addNewBuildingBlock),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, id]) =>
        this.buildingService.addBuildingBlock(action.buildingBlock).pipe(
          map(() => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: id! })),
          catchError((error) => of(ProjectActions.addNewBuildingFailure({ error: serializeError(error) })))
        )
      )
    )
  );

  deleteBuildingBlock$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.deleteBuildingBlock),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, id]) =>
        this.buildingService.deleteBuildingBlock(action.id).pipe(
          map(() => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: id! })),
          catchError((error) => of(ProjectActions.deleteBuildingBlockFailure({ error: serializeError(error) })))
        )
      )
    )
  );

  updateBuildingBlock$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.updateBuildingBlock),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, id]) =>
        this.buildingService.updateBuildingBlock(action.buildingBlock).pipe(
          map(() => ProjectActions.loadBuildingWithBuildingBlocks({ projectId: id! })),
          catchError((error) => of(ProjectActions.updateBuildingBlockFailure({ error: serializeError(error) })))
        )
      )
    )
  );

  getProjectTeam$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.getProjectTeam),
      concatMap((action) =>
        this.projectService.getProjectTeam(action.projectId).pipe(
          map(response => ProjectActions.getProjectTeamSuccess({ response })),
          catchError((error: any) =>
            of(ProjectActions.getProjectTeamFailure({error}))
          )
        )
      )
    );
  });

  setProjectTeam$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.setProjectTeam),
      concatMap((action) =>
        this.projectService.setProjectTeam(action.projectId, action.userIds).pipe(
          map(response => {
            this.store.dispatch(ProjectActions.getProjectTeam({projectId: action.projectId}));
            return ProjectActions.setProjectTeamSuccess();
          }),
          catchError((error: any) =>
            of(ProjectActions.setProjectTeamFailure({error}))
          )
        )
      )
    );
  });

  loadRequiredBuildingMaterials$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.loadRequiredMaterials),
      concatMap((action) =>
        this.resourceService.getRequiredMaterials(action.buildingId).pipe(
          map(result =>
            ProjectActions.loadRequiredMaterialsSuccess({ result: result })
          ),
          catchError((error: any) =>
            of(ProjectActions.loadRequiredMaterialsFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  })

  deleteRequiredMaterial$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.deleteRequiredMaterial),
      concatMap((action) =>
        this.resourceService.deleteRequiredMaterial(action.requiredMaterialId).pipe(
          map(result =>
            ProjectActions.deleteRequiredMaterialSuccess({ requiredMaterialId: action.requiredMaterialId })
          ),
          catchError((error: any) =>
            of(ProjectActions.deleteRequiredMaterialFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  })

  loadProjectDocuments$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.loadProjectDocuments),
      concatMap((action) =>
        this.projectService.getProjectDocuments(action.projectId, action.buildingId, action.sort, action.query, action.order).pipe(
          map(result =>
            ProjectActions.loadProjectDocumentsSuccess({ response: result })
          ),
          catchError((error: any) =>
            of(ProjectActions.loadProjectDocumentsFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  })

  uploadProjectDocuments$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.uploadProjectDocuments),
      concatMap((action) =>
        this.projectFileService.postDocuments(action.buildingId, action.files, action.urls).pipe(
          map(result => {
              this.store.dispatch(fromProjectActions.loadProjectDocuments({
                projectId: action.projectId,
                buildingId: action.buildingId,
                sort: 'created'
              }));
              return ProjectActions.uploadProjectDocumentsSuccess({ documents: result });
            }
          ),
          catchError((error: any) =>
            of(ProjectActions.uploadProjectDocumentsFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  });

  deleteProjectDocument$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.deleteProjectDocument),
      concatMap((action) =>
        this.projectService.deleteProjectDocument(action.projectDocumentId).pipe(
          map(result =>
            ProjectActions.deleteProjectDocumentSuccess({ response: result })
          ),
          catchError((error: any) =>
            of(ProjectActions.deleteProjectDocumentFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  })

  updateProjectDocument$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.updateProjectDocument),
      concatMap((action) =>
        this.projectService.updateProjectDocument(action.model).pipe(
          switchMap(result => of(
              ProjectActions.updateProjectDocumentSuccess({ response: result }),
              ModalDialogAction.closeModalDialog()
            )
          ),
          catchError((error: any) =>
            of(ProjectActions.updateProjectDocumentsFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  });

  postBuildingServices$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(ProjectActions.submitCheckedServices),
        mergeMap((action)=>
          this.resourcesService.postBuildingServices(action.services, action.buildingId).pipe(
            map((services)=> ProjectActions.submitCheckedServicesSuccess({services:services})),
            catchError((error)=>
              of(ProjectActions.submitCheckedServicesFailure({error: serializeError(error)}))
            )
          )
        )
      );
    }
  );

  getSelectedServices$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(ProjectActions.loadCheckedServices),
        mergeMap((action)=>
          this.resourcesService.getSelectedBuildingServices(action.buildingId).pipe(
            map((services)=> ProjectActions.loadCheckedServicesSuccess({services:services})),
            catchError((error)=>
              of(ProjectActions.loadCheckedServicesFailure({error: serializeError(error)}))
            )
          )
        )
      );
    }
  );
  getUnselectedServices$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(ProjectActions.loadUncheckedBuildingServices),
        mergeMap((action)=>
          this.resourcesService.getUnselectedBuildingServices(action.buildingId, action.filter).pipe(
            map((services)=> ProjectActions.loadUncheckedBuildingServicesSuccess(
              {services:services})),
            catchError((error)=>
              of(ProjectActions.loadUncheckedBuildingServicesFailure({error: serializeError(error)}))
            )
          )
        )
      );
    }
  );
  deleteBuildingService$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(ProjectActions.deleteBuildingService),
        mergeMap((action)=>
          this.resourcesService.deleteSelectedService(action.buildingId, action.serviceId).pipe(
            map(()=> {
              return ProjectActions.loadCheckedServices({buildingId: action.buildingId})
            }),
            catchError((error)=>
              of(ProjectActions.deleteBuildingServiceFailure({error: serializeError(error)}))
            )
          )
        )
      );
    }
  );

  saveRequiredMaterial$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.saveRequiredMaterials),
      concatMap((action) =>
        this.resourcesService.saveRequiredMaterials(action.materials).pipe(
            switchMap(res => {
              this.store.dispatch(ProjectActions.loadRequiredMaterials({ buildingId: action.materials[0].buildingId }));
              return of(ProjectActions.saveRequiredMaterialsSuccess());
            }),
            catchError((error: any) =>
            of(ProjectActions.saveRequiredMaterialsFailure({ error: serializeError(error) }))
          )
        )
      )
    )
  });

  loadPhasesForProjectOnInit$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.getDetailedProjectSuccess),
      concatMap((action) =>
        this.phaseService.getPhasesByProjectId(action.data.id).pipe(
          map((result) => ProjectActions.loadPhasesForProjectSuccess({phases: result})),
          catchError((error) => of(ProjectActions.loadPhasesForProjectFailure({error: serializeError(error)})))
        ))
    ));

  loadPhasesForProjectUpdate$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.loadPhasesForProject),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([_, projectId]) =>
        this.phaseService.getPhasesByProjectId(projectId!).pipe(
          map((result) => ProjectActions.loadPhasesForProjectSuccess({phases: result})),
          catchError(error => of(ProjectActions.loadPhasesForProjectFailure({error: serializeError(error)})))
        ))
    ))

  addPhase$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.addPhaseToProject),
      withLatestFrom(this.store.pipe(select(selectCurrentProjectId))),
      concatMap(([action, projectId]) =>
        this.phaseService.addPhaseToProject(action.phase, projectId!).pipe(
          map(() => ProjectActions.loadPhasesForProject()),
          catchError(error => of(ProjectActions.addPhaseToProjectFailure({error: serializeError(error)})))
        ))
    ));

  editPhase$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.editPhase),
      concatMap((action) =>
        this.phaseService.editPhase(action.phase).pipe(
          map(() => ProjectActions.loadPhasesForProject()),
          catchError(error => of(ProjectActions.editPhaseFailure({error: serializeError(error)})))
        ))
    ));

  editPhaseSteps$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.editPhase),
      concatMap((action) =>
        this.phaseService.editPhaseSteps(action.phase.id, action.phase.phaseSteps).pipe(
          map(() => ProjectActions.loadPhasesForProject()),
          catchError(error => of(ProjectActions.editPhaseFailure({error: serializeError(error)})))
        ))
    ))

  deletePhase = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.deletePhase),
      concatMap((action) =>
        this.phaseService.deletePhase(action.id).pipe(
          map(() => ProjectActions.loadPhasesForProject()),
          catchError(error => of(ProjectActions.deletePhaseFailure({error: serializeError(error)})))
        ))
    ));

  editPhaseStep$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProjectActions.updatePhaseStep),
      concatMap((action) =>
        this.phaseService.editPhaseStep(action.phaseStep).pipe(
          map(() => ProjectActions.loadPhasesForProject()),
          catchError(error => of(ProjectActions.updatePhaseStepFailure({error: serializeError(error)})))
        ))
    ));


  createReport$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectActions.createReport),
      concatMap((action) =>
        this.resourcesService.createReport(action.projectId).pipe(
          map(() => ProjectActions.createReportSuccess()),
          catchError((error: any) =>
            of(ProjectActions.createReportFailure({ error: serializeError(error) }))
          )
        )
      )
    );
  });

  initiallyLoadResources$ = createEffect(() =>
  this.actions$.pipe(
    ofType(ProjectActions.getDetailedProjectSuccess),
    map((action) => ProjectActions.loadUsedForProjectResources({projectId: action.data.id , sort: "", filter: ""}))
  ));

  loadingUsedMaterials$ = createEffect(() =>
      this.actions$.pipe(
        ofType(ProjectActions.loadUsedForProjectResources),
        mergeMap((action) =>
          this.resourcesService.getAllUsedMaterials( action.projectId , action.filter, action.sort).pipe(
            map((res) => ProjectActions.loadUsedForProjectResourcesSuccess({ materials: res })),
            catchError(error => of(ProjectActions.loadUsedForProjectResourcesFailure({ error: serializeError(error) })))
          )
        )
      )
  );

  constructor(
    private actions$: Actions,
    private projectService: ProjectService,
    private projectFileService: ProjectFileService,
    private resourceService: ResourceApiService,
    private errorService: ErrorService,
    private store: Store<AppState>,
    private buildingService: BuildingApiService,
    private resourcesService: ResourceApiService,
    private phaseService: PhaseApiService
  ) {
  }
}
