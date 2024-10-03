import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistoContaComponent } from './registo-conta.component';

describe('RegistoContaComponent', () => {
  let component: RegistoContaComponent;
  let fixture: ComponentFixture<RegistoContaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistoContaComponent]
    });
    fixture = TestBed.createComponent(RegistoContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
