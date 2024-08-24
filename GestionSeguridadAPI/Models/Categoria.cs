namespace GestionSeguridadAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int? CategoriaPadreId { get; set; } // Opcional para categorías principales
        public Categoria? CategoriaPadre { get; set; } // Opcional para categorías principales

        public ICollection<Categoria>? Subcategorias { get; set; } // Opcional
    }


}
