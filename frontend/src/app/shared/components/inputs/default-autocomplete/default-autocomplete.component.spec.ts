import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultAutocompleteComponent } from './default-autocomplete.component';

describe('DefaultAutocompleteComponent', () => {
  let component: DefaultAutocompleteComponent;
  let fixture: ComponentFixture<DefaultAutocompleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefaultAutocompleteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DefaultAutocompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
