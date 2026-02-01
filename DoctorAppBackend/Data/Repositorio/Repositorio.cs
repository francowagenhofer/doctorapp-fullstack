using Data.Interfaces.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    // Implementación genérica del repositorio que proporciona métodos comunes para las operaciones CRUD.
    public class Repositorio<T> : IRepositorioGenerico<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        private DbSet<T> _dbSet; // Representa el conjunto de entidades del tipo T en el contexto de la base de datos.

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null)
        {
             IQueryable<T> query = _dbSet; // Inicia la consulta con el conjunto de entidades del tipo T.
            
            if (filtro != null)
            {
                query = query.Where(filtro); // Aplica el filtro si se proporciona.
            }

            if (incluirPropiedades != null)
            {
                foreach (var ip in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(ip); // Incluye las propiedades relacionadas especificadas.
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(); // Aplica el ordenamiento si se proporciona.
            }

            return await query.ToListAsync(); // Devuelve la lista de entidades resultantes.
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = _dbSet; // Inicia la consulta con el conjunto de entidades del tipo T.

            if (filtro != null)
            {
                query = query.Where(filtro); // Aplica el filtro si se proporciona.
            }

            if (incluirPropiedades != null)
            {
                foreach (var ip in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(ip); // Incluye las propiedades relacionadas especificadas.
                }
            }

            return await query.FirstOrDefaultAsync(); // Devuelve la primera entidad que coincide con el filtro o null si no se encuentra ninguna.
        }

        public async Task Agregar(T entidad)
        {
            await _dbSet.AddAsync(entidad);
        }

        public void Remover(T entidad)
        {
             _dbSet.Remove(entidad);
        }
    }
}
