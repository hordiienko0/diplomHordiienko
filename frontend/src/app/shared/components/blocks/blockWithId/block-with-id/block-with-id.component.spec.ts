/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BlockWithIdComponent } from './block-with-id.component';

describe('BlockWithIdComponent', () => {
  let component: BlockWithIdComponent;
  let fixture: ComponentFixture<BlockWithIdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlockWithIdComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlockWithIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
