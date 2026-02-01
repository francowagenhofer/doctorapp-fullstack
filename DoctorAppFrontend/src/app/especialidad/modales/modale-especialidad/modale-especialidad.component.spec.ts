import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModaleEspecialidadComponent } from './modale-especialidad.component';

describe('ModaleEspecialidadComponent', () => {
  let component: ModaleEspecialidadComponent;
  let fixture: ComponentFixture<ModaleEspecialidadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModaleEspecialidadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModaleEspecialidadComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
