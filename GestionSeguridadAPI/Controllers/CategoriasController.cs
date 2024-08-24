using Microsoft.AspNetCore.Mvc;
using GestionSeguridadAPI.Models;
using GestionSeguridadAPI.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GestionSeguridadAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            // Si no se proporciona CategoriaPadreId, se trata de una categoría principal
            if (categoria.CategoriaPadreId.HasValue && categoria.CategoriaPadreId <= 0)
            {
                categoria.CategoriaPadreId = null;
            }

            var categoriaCreada = await _categoriaService.CrearCategoriaAsync(categoria);
            return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { id = categoriaCreada.Id }, categoriaCreada);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.ObtenerCategoriaPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var jsonResult = JsonSerializer.Serialize(categoria, options);


            return Ok(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategorias()
        {
            var categorias = await _categoriaService.ObtenerCategoriasAsync();
            return Ok(categorias);
        }
    }

}


