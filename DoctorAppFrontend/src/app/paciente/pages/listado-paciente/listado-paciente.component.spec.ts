import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListadoPacienteComponent } from './listado-paciente.component';

describe('ListadoPacienteComponent', () => {
  let component: ListadoPacienteComponent;
  let fixture: ComponentFixture<ListadoPacienteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListadoPacienteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListadoPacienteComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
