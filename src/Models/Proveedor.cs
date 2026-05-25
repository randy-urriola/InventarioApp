namespace InventarioApp.Models;
/// <sumary>
/// Record inmutable para representar proveedores a diferencia de las clases que si son mutables.
/// </sumary>
public record Proveedor
(
  int Id,
  string Nombre,
  string Email,
  string Telefono
);