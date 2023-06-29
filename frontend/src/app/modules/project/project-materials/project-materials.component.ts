import { Component, Input, OnInit } from '@angular/core';
import { AppState } from 'src/app/store';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { UsedByProjectMaterial } from '../resources/models/project-material/project-used-material.model';
import { ResourceApiService } from '../resources/services/resource-api.service';
import * as FromProjectSelectrs from '../state/project.selectors';

@Component({
  selector: 'app-project-materials[projectId]',
  templateUrl: './project-materials.component.html',
  styleUrls: ['./project-materials.component.scss'],
})
export class ProjectMaterialsComponent {
  @Input() projectId: number = 0;
  filter = "";
  sort = "";
  materials$ : Observable<UsedByProjectMaterial[]>;
  constructor(private apiService: ResourceApiService, private store: Store<AppState>) {
    console.log("CONSTRUUUUCTORRRR");
    this.materials$ = this.store.select(FromProjectSelectrs.selectUsedForProjectMaterials);
  }
}
