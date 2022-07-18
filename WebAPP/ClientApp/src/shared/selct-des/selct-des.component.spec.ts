import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelctDesComponent } from './selct-des.component';

describe('SelctDesComponent', () => {
  let component: SelctDesComponent;
  let fixture: ComponentFixture<SelctDesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelctDesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelctDesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
