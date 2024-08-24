
using GestionSeguridadAPI.Models;
using Microsoft.EntityFrameworkCore;


public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> CrearCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> ObtenerCategoriaPorIdAsync(int id)
    {
        return await _context.Categorias
            .Include(c => c.Subcategorias)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Categoria>> ObtenerCategoriasAsync()
    {
        return await _context.Categorias
            .Include(c => c.Subcategorias)
            .ToListAsync();
    }
}
