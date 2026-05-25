namespace InventarioApp.Models;
// Tipo de dato personalizado y se crea con enum

/// <sumary>
/// Ciclo de vida de un producto en el inventario.
/// </sumary>

public enum EstadoProducto
{
  /// <sumary>Disponible para venta.</sumary>
  Activo,
  /// <sumary>Temporalmente fuera de disponibilidad.</sumary> 
  Inactivo,
/// <sumary>Retirado permanentemente del catalogo.</sumary>
  Descontinuado
}