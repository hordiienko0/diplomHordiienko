import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageResourcesComponent } from './manage-resources.component';

describe('ResourcesPageComponent', () => {
  let component: ManageResourcesComponent;
  let fixture: ComponentFixture<ManageResourcesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageResourcesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageResourcesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
