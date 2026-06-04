using System.Text.Json;
using System.Text.Json.Serialization;
using InventarioApp.Infrastructure;
using InventarioApp.Models;

namespace InventarioApp.Infrastructure;

public class JsonInventarioStorage
{
  // Implementación de almacenamiento en JSON
  private readonly FileManager _fileManager;
  private readonly JsonSerializerOptions _options;

  // Ruta del archivo JSON
  public JsonInventarioStorage()
  {
    _fileManager = new FileManager();                           // Instancia del gestor de archivos
    _options = new JsonSerializerOptions                        // Configuración de serialización JSON
    {
      WriteIndented = true,                                     // Formateo legible
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,        // Convención camelCase
      Converters = { new JsonStringEnumConverter() }            // Soporte para enums como strings
    };
  }

  public void Guardar(List<Producto> productos, string ruta)
  {
    string json = JsonSerializer.Serialize(productos, _options);  // Serializa la lista de productos a JSON
    _fileManager.Escribir(ruta, json);                            // Escribe el JSON en el archivo
  }

  public List<Producto> Cargar(string ruta)
  {
    string json = _fileManager.Leer(ruta);                            // Lee el contenido del archivo JSON
    return JsonSerializer.Deserialize<List<Producto>>(json, _options) ?? new List<Producto>();  // Deserializa el JSON a una lista de productos
  }

// Método para crear un backup del archivo JSON
  public string CrearBackup(string ruta)
  {
    if (!_fileManager.Existe(ruta))
      return null;

    string directorio = Path.GetDirectoryName(ruta);
    string nombreSinExtension = Path.GetFileNameWithoutExtension(ruta);
    string extension = Path.GetExtension(ruta);
    string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

    string rutaBackup = Path.Combine(
      directorio ?? ".",
      $"{nombreSinExtension}_backup_{timestamp}{extension}"
    );

    File.Copy(ruta, rutaBackup);
    return rutaBackup;
  }
}