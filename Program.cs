using InventarioApp.Factories;
using InventarioApp.Repositories;
using InventarioApp.Models;
using InventarioApp.Infrastructure;


Console.WriteLine("=== InventarioApp ===");

var fileManager = new FileManager();
string contenido = "Inventario Actualizado";
fileManager.Escribir("inventario.txt", contenido);

string leerContenido = fileManager.Leer("inventario.txt");
Console.WriteLine(leerContenido);


var repositorio = new InMemoryProductoRepository();

var laptop = ProductoFactory.Crear("Laptop Dell XPS 13", 1200, 5, CategoriaProducto.Electronica);
var mouse = ProductoFactory.Crear("Mouse Logitech MX Master", 99, 20, CategoriaProducto.Electronica);
var teclado = ProductoFactory.Crear("Teclado Mecánico", 150, 3, CategoriaProducto.Electronica);
var silla = ProductoFactory.Crear("Silla Ergonómica Herman Miller", 500, 8, CategoriaProducto.Muebles);
var escritorio = ProductoFactory.Crear("Escritorio Stand-up", 300, 2, CategoriaProducto.Muebles);

repositorio.Agregar(laptop);
repositorio.Agregar(mouse);
repositorio.Agregar(teclado);
repositorio.Agregar(silla);
repositorio.Agregar(escritorio);

Console.WriteLine($"Productos agregados: {repositorio.Cantidad}\n");

// consultas básicas LINQ

var electronicos = repositorio.BuscarPorCategoria(CategoriaProducto.Electronica);
Console.WriteLine("=== Productos Electrónicos ===");

foreach (var producto in electronicos)
{
  Console.WriteLine($"- {producto.Nombre} : ${producto.Precio}");
}

var conMouse = repositorio.BuscarPorNombre("mouse");
Console.WriteLine("\nProductos con 'mouse' en el nombre: ");
foreach (var producto in conMouse)
{
  Console.WriteLine($"- {producto.Nombre}");
}

var nombres = repositorio.ObtenerNombres();
Console.WriteLine($"\n Todos los nombres: {string.Join(", ", nombres)}");

var hayStockBajo = repositorio.HayStockBajo();
Console.WriteLine($"\n Hay Stock Bajo? {hayStockBajo}");