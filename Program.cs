using InventarioApp.Factories;
using InventarioApp.Repositories;
using InventarioApp.Models;
using InventarioApp.Infrastructure;


Console.WriteLine("=== Prueba integración JSON ===");

var almacenamiento = new JsonInventarioStorage();

var productos = new List<Producto>
{
  new Producto
  {
    Id = 1,
    Nombre = "Laptop",
    Precio = 999.99m,
    Cantidad = 10,
    Categoria = CategoriaProducto.Electronica,
    Estado = EstadoProducto.Activo
    },
  new Producto
  {
    Id = 2,
    Nombre = "Camiseta",
    Precio = 19.99m,
    Cantidad = 50,
    Categoria = CategoriaProducto.Ropa,
    Estado = EstadoProducto.Activo
  }
};

string ruta = "inventario.json";

almacenamiento.CrearBackup(ruta);
almacenamiento.Guardar(productos, ruta);

Console.WriteLine("Inventario guardado correctamente.");

var productosCargados = almacenamiento.Cargar(ruta);

Console.WriteLine("Inventario cargado correctamente:");
foreach (var p in productosCargados)
{
  Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Precio: {p.Precio}, Cantidad: {p.Cantidad}, Categoria: {p.Categoria}, Estado: {p.Estado}");
}