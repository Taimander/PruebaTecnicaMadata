namespace ASPMVC.Models
{
    public class Producto
    {
        public Producto(
                int Id,
                string Nombre,
                string Descripcion,
                decimal Precio,
                string Categoria
            )
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Precio = Precio;
            this.Categoria = Categoria;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }

    }
}
