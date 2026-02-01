using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    // Interfaz específica para el repositorio de Especialidad que extiende la interfaz genérica
    public interface IMedicoRepositorio : IRepositorioGenerico<Medico>
    {
        void Actualizar(Medico medico);
    }
}
