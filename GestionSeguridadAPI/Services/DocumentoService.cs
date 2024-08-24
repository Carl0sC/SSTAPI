using GestionSeguridadAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionSeguridadAPI.Services
{
    public class DocumentoService
    {
        private readonly AppDbContext _context;

        // Constructor que recibe el contexto de base de datos
        public DocumentoService(AppDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los documentos
        public async Task<List<Documento>> GetDocumentosAsync()
        {
            return await _context.Documentos.ToListAsync(); // Asegúrate de que este nombre coincida
        }

        // Método para obtener un documento por su Id
        public async Task<Documento> GetDocumentoByIdAsync(int id)
        {
            return await _context.Documentos.FindAsync(id);
        }

        // Método para agregar un nuevo documento
        public async Task AddDocumentoAsync(Documento documento)
        {
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar un documento existente
        public async Task UpdateDocumentoAsync(Documento documento)
        {
            _context.Documentos.Update(documento);
            await _context.SaveChangesAsync();
        }

        // Método para eliminar un documento por su Id
        public async Task DeleteDocumentoAsync(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento != null)
            {
                _context.Documentos.Remove(documento);
                await _context.SaveChangesAsync();
            }
        }

        // Otros métodos adicionales según sea necesario
    }
}
