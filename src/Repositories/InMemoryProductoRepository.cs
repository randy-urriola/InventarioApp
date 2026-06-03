namespace InventarioApp.Repositories;

using InventarioApp.Models;
using System.Linq;

public class InMemoryProductoRepository : IProductoRepository
{
  private readonly List<Producto> _productos = new();  // mantiene los productos durante la sesión
  private int _proximoId = 1;  // contador para asignar id únicos

  public void Agregar(Producto producto)  // nuevo producto y se incrementa id
  {
    producto.Id = _proximoId++;
    _productos.Add(producto);
  }

  public Producto? ObtenerPorId(int id)  // obtener por id asignado, el primero que cumple la condición, si no hay retorna null
  {
    return _productos.FirstOrDefault((Producto p) => p.Id == id);
  }

  public IEnumerable<Producto> ObtenerTodos()
  {
    return _productos.AsReadOnly();  // obtiene todos los datos pero solo como lectura, protegiendo de modificaciones, retorna IEnumerable para mantener flexibilidad en futuros cambios
  }

  public bool Actualizar(Producto producto)
  {
    Producto? existente = ObtenerPorId(producto.Id);
    if (existente == null) return false;

    existente.Nombre = producto.Nombre;
    existente.Precio = producto.Precio;
    existente.Cantidad = producto.Cantidad;
    existente.Categoria = producto.Categoria;
    existente.Estado = producto.Estado;

    return true;
  }

  public bool Eliminar(int id)
  {
    Producto? producto = ObtenerPorId(id);
    if (producto == null) return false;

    return _productos.Remove(producto);
  }

  public int Cantidad => _productos.Count;

  //===================== Búsquedas con where LINQ =====================
  public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
  {
    return _productos.Where((Producto p) => p.Categoria == categoria);
  }

  public IEnumerable<Producto> BuscarPorNombre(string nombre)
  {
    return _productos.Where((Producto p) => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
  }

  public IEnumerable<Producto> BuscarPorRangoPrecio(decimal precioMinimo, decimal precioMaximo)
  {
    return _productos.Where((Producto p) => p.Precio >= precioMinimo && p.Precio <= precioMaximo);
  }

  //===================== Select y Any =====================
  public IEnumerable<string> ObtenerNombres()
  {
    return _productos.Select((Producto p) => p.Nombre);
  }

  public bool HayStockBajo()
  {
    return _productos.Any((Producto p) => p.Cantidad < 5);
  }

  public IEnumerable<Producto> OrdenarPorPrecio()
  {
    return _productos.OrderBy((Producto p) => p.Precio);
  }

  public IEnumerable<Producto> OrdenarTopPorPrecio(int cantidad)
  {
    return _productos.OrderByDescending((Producto p) => p.Precio).Take(cantidad); // take para limitar a los top n productos
  }

  //===================== GroupBy y conversion a Dictionary =====================
  public IEnumerable<IGrouping<CategoriaProducto, Producto>> AgruparPorCategoria() // IGrouping es similar a dictionary, tiene una key que es la categoría y un item que son los productos del grupo
  {
    return _productos.GroupBy((Producto p) => p.Categoria);
  }

  public Dictionary<CategoriaProducto, int> ContarPorCategoria()
  {
    return _productos.
    GroupBy((Producto p) => p.Categoria)
    .ToDictionary(g => g.Key, g => g.Count());
  }

  //===================== Agregar con Sum, Average y MaxBy =====================
  public decimal ObtenerValorTotalInventario()
  {
    return _productos.Sum((Producto p) => p.ValorTotal);
  }

  public decimal ObtenerPrecioPromedio()
  {
    if (_productos.Count == 0) return 0; // evitar división por cero
    return _productos.Average((Producto p) => p.Precio);
  }

  public Producto? ObtenerProductoMasCaro()
  {
    return _productos.MaxBy((Producto p) => p.Precio);
  }

  public Dictionary<CategoriaProducto, decimal> ObtenerValorPorCategoria()
  {
    return _productos
    .GroupBy((Producto p) => p.Categoria)
    .ToDictionary(g => g.Key, g => g.Sum(p => p.ValorTotal));
  }
  
  public IEnumerable<Producto> ObtenerStockBajo(int umbral = 5)
  {
    return _productos.Where((Producto p) => p.Cantidad < umbral);
  }
}