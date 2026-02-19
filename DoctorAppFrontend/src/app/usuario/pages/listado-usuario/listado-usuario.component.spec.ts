import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListadoUsuarioComponent } from './listado-usuario.component';

describe('ListadoUsuarioComponent', () => {
  let component: ListadoUsuarioComponent;
  let fixture: ComponentFixture<ListadoUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListadoUsuarioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListadoUsuarioComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
