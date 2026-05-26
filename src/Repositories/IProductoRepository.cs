namespace InventarioApp.Repositories;

using InventarioApp.Models;

/// <sumary>
/// Contrato para almacenamiento de productos.
// Define las operaciones básicas CRUD.
/// </sumary>
public interface IProductoRepository
{
  /// <sumary>
  /// Agrega un producto al repositorio.
  /// </sumary>
  void Agregar(Producto producto);

  /// <sumary>
  /// Obtiene un producto por s id.
  /// Retorna null si no existe.
  /// </sumary>
  Producto? ObtenerPorId(int id);
  
  /// <sumary>
  /// Obtiene todos los productos.
  /// </sumary>
  IEnumerable<Producto> ObtenerTodos();

  /// <sumary>
  /// Actualiza un producto existente.
  /// </sumary>
  bool Eliminar(int id);

  /// <sumary>
  /// Cantidad total de productos en el repositorio.
  /// </sumary>
  int Cantidad{ get; }
}