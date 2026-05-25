namespace InventarioApp.Models;

public class Producto
{
  private string _nombre = "";
  private decimal _precio;
  private int _cantidad;
  public int Id { get; set; }

  //====================== Validación con guard clauses ======================
  public string Nombre
  {
    get => _nombre;
    set
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Nombre no puede estar vacío", nameof( Nombre ));
      _nombre = value.Trim();
    }
    }
  public decimal Precio
  {
    get => _precio;
    set
    {
      if (value < 0)
        throw new ArgumentException("Precio no puede ser negativo", nameof(Precio));
      _precio = value;
    }
  } 
  public int Cantidad
  {
    get => _cantidad;
    set
    {
      if (value < 0)
        throw new ArgumentException("Cantidad no puede ser negativa", nameof(Cantidad));
      _cantidad = value;
    }
  } 
  //===========================================================================
  public CategoriaProducto Categoria { get; set; }
  public EstadoProducto Estado { get; set; } = EstadoProducto.Activo;         // Tipos de datos que se deben crear
  public DateTime FechaRegistro { get; set; } = DateTime.Now;

  public decimal ValorTotal => Precio * Cantidad;
}