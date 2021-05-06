import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavMenuMainComponent } from './nav-menu-main.component';

describe('NavMenuMainComponent', () => {
  let component: NavMenuMainComponent;
  let fixture: ComponentFixture<NavMenuMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavMenuMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
