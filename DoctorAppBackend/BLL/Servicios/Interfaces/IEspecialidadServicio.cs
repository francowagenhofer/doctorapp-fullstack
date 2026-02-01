using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IEspecialidadServicio
    {
        Task<IEnumerable<EspecialidadDTO>> ObtenerTodos();
        Task<IEnumerable<EspecialidadDTO>> ObtenerActivos();

        Task<EspecialidadDTO> Agregar(EspecialidadDTO modeloDto);

        Task Actualizar(EspecialidadDTO modeloDto);

        Task Remover(int id);

    }
}
