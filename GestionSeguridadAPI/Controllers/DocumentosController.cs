using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionSeguridadAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace GestionSeguridadAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documento>>> GetDocumentos()
        {
            return await _context.Documentos.ToListAsync();
        }

        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> GetDocumento(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);

            if (documento == null)
            {
                return NotFound();
            }

            return documento;
        }

        // POST: api/Documentos
        [HttpPost]
        public async Task<ActionResult<Documento>> PostDocumento(
            [FromForm] IFormFile archivo,
            [FromForm] string nombre,
            [FromForm] int categoriaId,
            [FromForm] int usuarioId)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha subido ningún archivo.");
            }

            // Leer el archivo y convertirlo a un array de bytes
            byte[] contenido;
            using (var memoryStream = new MemoryStream())
            {
                await archivo.CopyToAsync(memoryStream);
                contenido = memoryStream.ToArray();
            }

            // Crear un nuevo documento
            var documento = new Documento
            {
                Nombre = nombre,
                Contenido = contenido,
                CategoriaId = categoriaId,
                FechaSubida = DateTime.UtcNow,
                UsuarioId = usuarioId
            };

            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumento), new { id = documento.Id }, documento);
        }

        // PUT: api/Documentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(int id, Documento documento)
        {
            if (id != documento.Id)
            {
                return BadRequest();
            }

            _context.Entry(documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }

            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
