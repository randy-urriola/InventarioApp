namespace InventarioApp.Models;

public class Producto
{
  public int Id { get; set; }
  public string Nombre { get; set; } = "";
  public string Descripcion { get; set; } = "";
  public decimal Precio { get; set; } 
  public int Cantidad { get; set; } 
  public CategoriaProducto Categoria { get; set; }
  public EstadoProducto Estado { get; set; } = EstadoProducto.Activo;         // Tipos de datos que se deben crear
  public DateTime FechaRegistro { get; set; } = DateTime.Now;

  public decimal ValorTotal => Precio * Cantidad;

  public override string ToString()
  => $"[{Id}] {Nombre} - ${Precio:N2} x {Cantidad} = ${ValorTotal:N2}";       // usa el método ToString con override para dar formato de salida
}