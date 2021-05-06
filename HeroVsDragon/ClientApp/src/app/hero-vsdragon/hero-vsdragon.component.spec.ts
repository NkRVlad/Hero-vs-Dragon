import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroVsdragonComponent } from './hero-vsdragon.component';

describe('HeroVsdragonComponent', () => {
  let component: HeroVsdragonComponent;
  let fixture: ComponentFixture<HeroVsdragonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroVsdragonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroVsdragonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
