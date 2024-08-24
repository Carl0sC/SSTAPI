using GestionSeguridadAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionSeguridadAPI.Services
{
    public interface IDocumentoService
    {
        Task<Documento> GetDocumentoByIdAsync(int id);
        Task<IEnumerable<Documento>> GetAllDocumentosAsync();
        Task AddDocumentoAsync(Documento documento);
        Task UpdateDocumentoAsync(Documento documento);
        Task DeleteDocumentoAsync(int id);
    }
}
