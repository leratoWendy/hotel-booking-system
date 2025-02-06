import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminGuestsListComponent } from './admin-guests-list.component';

describe('AdminGuestsListComponent', () => {
  let component: AdminGuestsListComponent;
  let fixture: ComponentFixture<AdminGuestsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminGuestsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminGuestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
