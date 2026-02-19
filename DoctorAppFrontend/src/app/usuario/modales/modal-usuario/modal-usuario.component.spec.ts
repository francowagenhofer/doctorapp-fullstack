import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalUsuarioComponent } from './modal-usuario.component';

describe('ModalUsuarioComponent', () => {
  let component: ModalUsuarioComponent;
  let fixture: ComponentFixture<ModalUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModalUsuarioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalUsuarioComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
