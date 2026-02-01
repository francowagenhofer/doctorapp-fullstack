using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    // Interfaz para la unidad de trabajo que coordina los repositorios.
    // Proporciona una forma de agrupar múltiples operaciones de repos.
    public interface IUnidadTrabajo : IDisposable
    {
        IEspecialidadRepositorio Especialidad { get; }
        IMedicoRepositorio Medico { get; }
        Task Guardar();
    }
}
