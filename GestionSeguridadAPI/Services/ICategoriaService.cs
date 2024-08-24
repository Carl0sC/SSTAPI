using GestionSeguridadAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoriaService
{
    Task<Categoria> CrearCategoriaAsync(Categoria categoria);
    Task<Categoria> ObtenerCategoriaPorIdAsync(int id);
    Task<IEnumerable<Categoria>> ObtenerCategoriasAsync();
}
