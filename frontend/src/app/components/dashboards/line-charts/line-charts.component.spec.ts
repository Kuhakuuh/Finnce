import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LineChartsComponent } from './line-charts.component';

describe('LineChartsComponent', () => {
  let component: LineChartsComponent;
  let fixture: ComponentFixture<LineChartsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LineChartsComponent]
    });
    fixture = TestBed.createComponent(LineChartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
