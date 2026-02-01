import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListadoEspecialidad } from './listado-especialidad';

describe('ListadoEspecialidad', () => {
  let component: ListadoEspecialidad;
  let fixture: ComponentFixture<ListadoEspecialidad>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListadoEspecialidad]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListadoEspecialidad);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
