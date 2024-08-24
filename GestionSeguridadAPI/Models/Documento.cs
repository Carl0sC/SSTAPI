namespace GestionSeguridadAPI.Models
{
    public class Documento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime FechaSubida { get; set; }
        public int UsuarioId { get; set; } // Ajusta según cómo manejes los usuarios
    }

}
