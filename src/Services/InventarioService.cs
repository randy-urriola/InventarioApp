using InventarioApp.Repositories;
using InventarioApp.Models;
using InventarioApp.Infrastructure;
using InventarioApp.Factories;
using System.Net.Http.Headers;

namespace InventarioApp.Services;

public class InventarioService
{
  private readonly InMemoryProductoRepository _repository;
  private readonly JsonInventarioStorage _storage;
  private readonly string _rutaInventario;

  // Constructor
  public InventarioService(string rutaInventario = "inventario.json")
  {
    _repository = new InMemoryProductoRepository();
    _storage = new JsonInventarioStorage();
    _rutaInventario = rutaInventario;

    CargarInventario();
  }

  // Cargar productos del inventario
  private void CargarInventario()
  {
    if (File.Exists(_rutaInventario))
    {
      var productos = _storage.Cargar(_rutaInventario);
      foreach (var producto in productos)
      {
        _repository.Agregar(producto);
      }
    }
  }

  // Agregar un producto
  public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
  {
    var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
    _repository.Agregar(producto);
    _Persistir();
  }

  // Trae todos los productos desde el repositorio como IEnumerable
  public IEnumerable<Producto> ObtenerTodosLosProductos()
  {
    return _repository.ObtenerTodos();
  }

  // Trae un porducto por su Id (puede ser null)
  public Producto? ObtenerProductoPorId(int id)
  {
    return _repository.ObtenerPorId(id);
  }

  // Actualiza un porducto específico
  public void ActualizarProducto(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
  {
    var producto = _repository.ObtenerPorId(id);
    if (producto != null)
    {
      producto.Nombre = nombre;
      producto.Precio = precio;
      producto.Cantidad = cantidad;
      producto.Categoria = categoria;
      _repository.Actualizar(producto);
      _Persistir();
    }
  }

  // Eliminar un producto específico
  public void EliminarProducto(int id)
  {
    _repository.Eliminar(id);
    _Persistir();
  }


//=========== Métodos para buscar productos por diferentes formas ===========/
  public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
  {
    return _repository.BuscarPorCategoria(categoria);
  }

  public IEnumerable<Producto> BuscarPorNombre(string nombre)
  {
    return _repository.BuscarPorNombre(nombre);
  }

  public IEnumerable<Producto> ObtenerProductoBajoStock(int minimo = 5)
  {
    return _repository.ObtenerStockBajo(minimo);
  }

  public decimal ObtenerValorTotalIntentario()
  {
    return _repository.ObtenerValorTotalInventario();
  }

  public decimal ObtenerPrecioPromedio()
  {
    return _repository.ObtenerPrecioPromedio();
  }

  public Producto? ObtenerProductoMasCaro()
  {
    return _repository.ObtenerProductoMasCaro();
  }

  //=========== Métodos de reportes ===========/
  public string GenerarResumen()
  {
    var productos = _repository.ObtenerTodos();
    var generador = new GeneradorReportes(productos);
    return generador.GenerarResumen();
  }

  public string GenerarReporteStockBajo(int minimo = 5)
  {
    var productos = _repository.ObtenerTodos();
    var generador = new GeneradorReportes(productos);
    return generador.GenerarReporteStockBajo();
  }

  public string GenerarTopProductos(int minimo = 5)
  {
    var productos = _repository.ObtenerTodos();
    var generador = new GeneradorReportes(productos);
    return generador.GenerarTopProductos();
  }

  public string ExportarCsv()
  {
    var productos = _repository.ObtenerTodos();
    var generador = new GeneradorReportes(productos);
    return generador.ExportarCsv();
  }
  
  // Crea un backup de tipo json de todo el inventario
  private void _Persistir()
  {
    _storage.CrearBackup(_rutaInventario);
    var productos = _repository.ObtenerTodos().ToList();
    _storage.Guardar(productos, _rutaInventario);
  }

}