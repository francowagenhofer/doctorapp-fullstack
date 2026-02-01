import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListadoMedicoComponent } from './listado-medico.component';

describe('ListadoMedicoComponent', () => {
  let component: ListadoMedicoComponent;
  let fixture: ComponentFixture<ListadoMedicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListadoMedicoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListadoMedicoComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
