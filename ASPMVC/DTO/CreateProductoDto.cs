namespace ASPMVC.DTO
{
    public class CreateProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }

        public CreateProductoDto(
            string Nombre,
            string Descripcion,
            decimal Precio,
            string Categoria
            )
        {
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Precio = Precio;
            this.Categoria = Categoria;
        }

    }
}
