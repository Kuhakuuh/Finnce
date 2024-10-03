import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationpopupComponent } from './notificationpopup.component';

describe('NotificationpopupComponent', () => {
  let component: NotificationpopupComponent;
  let fixture: ComponentFixture<NotificationpopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NotificationpopupComponent]
    });
    fixture = TestBed.createComponent(NotificationpopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
