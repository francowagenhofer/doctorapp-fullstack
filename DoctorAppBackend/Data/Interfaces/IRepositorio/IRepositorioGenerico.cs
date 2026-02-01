using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    // Interfaz genérica para el repositorio. Define los métodos comunes para las operaciones CRUD.
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null, // filtro de búsqueda
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // ordenamiento de resultados
            string incluirPropiedades = null // inclusiones de propiedades relacionadas
        );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null, // filtro de búsqueda
            string incluirPropiedades = null // inclusiones de propiedades relacionadas
        );

        Task Agregar (T entidad); // Agrega una nueva entidad
        void Remover (T entidad); // Remueve una entidad existente

    }
}
