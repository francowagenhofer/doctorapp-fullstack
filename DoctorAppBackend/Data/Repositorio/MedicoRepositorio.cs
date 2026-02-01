using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    // Clase que implementa el repositorio para la entidad Especialidad
    public class MedicoRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MedicoRepositorio(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        // Método para actualizar un médico existente
        public void Actualizar(Medico medico)
        {
            var medicoDb = _db.Medicos.FirstOrDefault(e => e.Id == medico.Id);

            if (medicoDb != null)
            {
                medicoDb.Nombres = medico.Nombres;
                medicoDb.Apellidos = medico.Apellidos;
                medicoDb.Direccion = medico.Direccion;
                medicoDb.Telefono = medico.Telefono;
                medicoDb.Genero = medico.Genero;
                medicoDb.Estado = medico.Estado;
                medicoDb.EspecialidadId = medico.EspecialidadId;
                medicoDb.FechaActualizacion = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}
