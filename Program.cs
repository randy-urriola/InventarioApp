using InventarioApp.Models;
using InventarioApp.Services;

var servicio = new InventarioService();
bool activo = true;

while (activo)
{
  MostrarMenu();
  string opction = Console.ReadLine() ?? "";

  switch (opction)
  {
    case "1":
      AgregarProducto();
      break;
    case "2":
      ListarProductos();
      break;
    case "3":
      BuscarPorId();
      break;
    case "4":
      EliminarProducto();
      break;
    case "5":
      BuscarPorCategoria();
      break;
    case "6":
      MostrarResumen();
      break;
    case "7":
      MostrarStockBajo();
      break;
    case "8":
      MostrarEstadistica();
      break;
    case "9":
      ExportarCsv();
      break;
    case "10":
      activo = false;
      Console.WriteLine("\nHasta luego!");
      break;
    default:
      Console.WriteLine("\nOpción no válida.");
      break;
  }
  continue;
  void MostrarMenu()
  {
    Console.WriteLine("\n=== SISTEMA DE INVENTARIO ===");
    Console.WriteLine("1. Agregar producto");
    Console.WriteLine("2. Listar producto");
    Console.WriteLine("3. Buscar por ID");
    Console.WriteLine("4. Eliminar producto");
    Console.WriteLine("5. Buscar por categoría");
    Console.WriteLine("6. Ver resumen");
    Console.WriteLine("7. Ver stock bajo");
    Console.WriteLine("8. Ver estadística");
    Console.WriteLine("9. Exportar CSV");
    Console.WriteLine("10. Salir");
    Console.WriteLine("\nSelecciona una opción: ");
  }

  void AgregarProducto()
  {
    Console.Write("\nNombre: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Precio: ");
    decimal precio = decimal.Parse(Console.ReadLine() ?? "0");

    Console.Write("Cantidad: ");
    int cantidad = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("\nCategoría: Electronica, Ropa, Alimentos, Hogar, Deportes, Libors, Muebles, Otros");
    Console.Write("Categoría: ");
    string categoriaStr = Console.ReadLine() ?? "Otros";

    if (Enum.TryParse<CategoriaProducto>(categoriaStr, true, out var categoria))
    {
      servicio.AgregarProducto(nombre, precio, cantidad, categoria);
      Console.WriteLine("\nProducto agregado exitosamente.");
    }
    else
    {
      Console.WriteLine("\nCategoria no válida.");
    }
  }
  
  void ListarProductos()
  {
    var productos = servicio.ObtenerTodosLosProductos();
    if (!productos.Any())
    {
      Console.WriteLine("\nNo hay productos.");
      return;
    }

    Console.WriteLine("\n=== PRODUCTOS ===");
    foreach (var producto in productos)
    {
      Console.WriteLine($"ID: {producto.Id} | {producto.Nombre} | Precio: {producto.Precio} | Cantidiad: {producto.Cantidad} | Total: {producto.ValorTotal}");
    }
  }
  
  void BuscarPorId()
  {
    Console.Write("\nID del producto: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    var producto = servicio.ObtenerProductoPorId(id);
    if (producto != null)
    {
      Console.WriteLine($"\nID: {producto.Id}");
      Console.WriteLine($"Nombre: {producto.Nombre}");
      Console.WriteLine($"Precio: {producto.Precio}");
      Console.WriteLine($"Cantidad: {producto.Cantidad}");
      Console.WriteLine($"Valor Total: ${producto.ValorTotal}");
      Console.WriteLine($"Categoria: {producto.Categoria}");
    }
    else
    {
      Console.WriteLine("\nProducto no encontrado.");
    }
  }
  
  void EliminarProducto()
  {
    Console.Write("\nID del producto a eliminar: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    var producto = servicio.ObtenerProductoPorId(id);
    if (producto != null)
    {
      servicio.EliminarProducto(id);
      Console.WriteLine("\nProducto eliminado.");
    }
    else
    {
      Console.WriteLine("\nProducto no encontrado.");
    }
  }

  void BuscarPorCategoria()
  {
    Console.WriteLine("\nCategoría: Electronica, Ropa, Alimentos, Hogar, Deportes, Libors, Muebles, Otros");
    Console.Write("Categoría: ");
    string categoriaStr = Console.ReadLine() ?? "Otros";

    if (Enum.TryParse<CategoriaProducto>(categoriaStr, true, out var categoria))
    {
      var productos = servicio.BuscarPorCategoria(categoria);
      if (!productos.Any())
      {
        Console.WriteLine("\nNo hay productos en esta categoria.");
        return;
      }

      Console.WriteLine($"\n=== PRODUCTOS EN {categoria} ===");
      foreach (var producto in productos)
      {
        Console.WriteLine($"ID: {producto.Id} | {producto.Nombre} | Precio: {producto.Precio} | Cantidiad: {producto.Cantidad}");
      }
    }
    else
    {
      Console.WriteLine("\nCategoria no válida.");
    }
  }
  
  void MostrarResumen()
  {
    var resumen = servicio.GenerarResumen();
    Console.WriteLine($"\n{resumen}");
  }

  void MostrarStockBajo()
  {
    var reporte = servicio.GenerarReporteStockBajo();
    Console.WriteLine($"\n{reporte}");
  }

  void MostrarEstadistica()
  {
    Console.WriteLine("\n=== ESTADÍSTICA");
    Console.WriteLine($"Valor Total del inventario: {servicio.ObtenerValorTotalIntentario()}");
    Console.WriteLine($"Precio promedio: {servicio.ObtenerPrecioPromedio():F2}");

    var masCaro = servicio.ObtenerProductoMasCaro();
    if (masCaro != null)
    {
      Console.WriteLine($"Producto más caro: {masCaro.Nombre} (${masCaro.Precio})");
    }
  }
  
  void ExportarCsv()
  {
    string csv = servicio.ExportarCsv();
    Console.WriteLine($"\n{csv}");
  }
}