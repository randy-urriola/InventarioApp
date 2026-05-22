
// ============================================================
// SISTEMA DE INVENTARIO - Clase 1.1
// Estado: Mensaje de bienvenida
// ============================================================

using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

MostrarBanner();

if (args.Length > 0)
{
  switch (args[0].ToLower())
  {
    case "--help":
      MostrarAyuda();
      Environment.Exit(0);
      break;

    case "--version":
      Console.WriteLine($"InventarioApp v[{version}]");
      Environment.Exit(0); // ejecución exitosa
      break;

    default:
      Console.WriteLine($"Error: Comando desconocido '{args[0]}'");
      Console.WriteLine("Use --help para ver los comandos disponibles.");
      Environment.Exit(2); // error de argumentos
      break;
  }
}

// Variables

int cantidadProductos = 0;
decimal valorTotalDelInventario = 0.00m;
bool sistemaActivo = true;
string nombreSistema = "Sistema de Gestión de Inventario";
// decimal precio = 19.99m;


Console.WriteLine("Estado del Sistema");
Console.WriteLine($"  Nombre: {nombreSistema}");
Console.WriteLine($"  Productos registrados: {cantidadProductos}");
Console.WriteLine($"  Valor total del inventario: {valorTotalDelInventario:N2}"); // :N2 indica la cantidad de decimales y separadores de miles
Console.WriteLine($"  Sistema activo: {(sistemaActivo ? "Sí" : "No")}");

// Conversiones
Console.Write("Ingrese una cantidad: ");
string? entradaCantidad = Console.ReadLine();

// Conversion segura TryParse
if (int.TryParse(entradaCantidad, out int cantidad))
{
  Console.WriteLine($"Cantidad valida: {cantidad}");
  cantidadProductos = cantidad;
}
else
{
  Console.WriteLine("Error: debe ingresar un numero entero");
}

Console.Write("Ingrese un precio: ");
string? entradaPrecio = Console.ReadLine();

if (decimal.TryParse(entradaPrecio, out decimal precio2))
{
  Console.Write($"Precio valido: {precio2:N2}\n");
  valorTotalDelInventario = cantidadProductos * precio2;
  Console.WriteLine($"Valor total del inventario actualizado: ${valorTotalDelInventario:N2}");
}
else
{
  Console.WriteLine("Error: debe ingresar un numero decimal");
}

// Loop de nullabilidad

Console.WriteLine("Comandos: listar, agregar, busca, salir");
Console.WriteLine();

while (sistemaActivo)
{
  Console.Write("inventario: ");
  string? entrada = Console.ReadLine();

  // Aplicando el manejo seguro
  string comando = string.IsNullOrEmpty(entrada) ? "salir" : entrada.Trim().ToLower();
  switch (comando)
  {
    case "salir":
      sistemaActivo = false;
      Console.WriteLine("Hasta luego");
      break;

    case "listar":
      Console.WriteLine($"Productos del inventario: {cantidadProductos}");
      break;

    case "":
      break;

    default:
      Console.WriteLine($"Comando '{comando}' no reconocido.");
      Console.WriteLine("Comandos disponibles: listar, agregar, buscar, salir");
      break;
  }
}

// Modo interactivo si no hay argumentos
/*Console.Write("Ingrese un comando (o 'salir' para terminar): ");
string? entrada = Console.ReadLine(); // STDIN, con el ? acepta entradas nulas

if (string.IsNullOrWhiteSpace(entrada) || entrada.ToLower() == "salir")
{
  Console.WriteLine("Hasta luego!"); // STDOut para mostrar salida
  Environment.Exit(0);
}


Console.WriteLine("Estructura del proyecto:");
Console.WriteLine("  InventarioApp/");
Console.WriteLine("   |-- Program.cs");
Console.WriteLine("   |-- InventarioApp.csproj");
Console.WriteLine("   |-- gitignore");
Console.WriteLine("   |-- README.md");
Console.WriteLine("   |-- src/");
Console.WriteLine("       |-- Models/ (próxima clase)");
Console.WriteLine("Configuración .csproj");
Console.WriteLine("Carpeta src/ creada");
Console.WriteLine("Metadatos configurados");
Console.WriteLine();
Console.WriteLine("Próximo paso: Checkpoint"); */

// ====================== Funciones ======================

void MostrarBanner()
{
  Console.WriteLine("==========================================");
Console.WriteLine("    SISTEMA DE GESTIÓN DE INVENTARIO      ");
Console.WriteLine("==========================================");
Console.WriteLine();
Console.WriteLine($"Versión: {version}");
Console.WriteLine($"Plataforma: {Environment.OSVersion}");
Console.WriteLine($".NET Version: {Environment.Version}");
Console.WriteLine();
}

void MostrarAyuda()
{
  Console.WriteLine("USO: InventarioApp [comando] [opciones]");
    Console.WriteLine();
    Console.WriteLine("COMANDOS:");
    Console.WriteLine("  --help, -h      Muestra esta ayuda");
    Console.WriteLine("  --version, -v   Muestra la version del programa");
    Console.WriteLine();
    Console.WriteLine("EJEMPLOS:");
    Console.WriteLine(" dotnet run -- --help");
    Console.WriteLine(" dotnet run -- --version");
}