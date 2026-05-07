using System.Net.Http.Headers;
using System.Text.Json;

namespace LoadVIS
{
    public class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            try
            {
                var visitas = await ObtenerVisitas();

                if (visitas != null && visitas.Any())
                {
                    var personasMorales = new List<PersonaMoral>();

                    foreach (var visita in visitas)
                    {
                        var persona = await ObtenerPersonaMoral(visita.ClavePes, visita.IdSector);
                        if (persona != null && persona.Any())
                        {
                            personasMorales.AddRange(persona);
                        }
                    }

                    Console.WriteLine($"Se procesaron {visitas.Count} visitas");
                    Console.WriteLine($"Se encontraron {personasMorales.Count} personas morales");

                    // Mostrar ejemplo del primer resultado
                    if (personasMorales.Any())
                    {
                        Console.WriteLine("\nPrimer resultado:");
                        var json = JsonSerializer.Serialize(personasMorales.First(), new JsonSerializerOptions { WriteIndented = true });
                        Console.WriteLine(json);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static async Task<List<Visita>?> ObtenerVisitas()
        {
            var token = Environment.GetEnvironmentVariable("AZURE_TOKEN");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Add("X-Ordenamiento-Campo", "Id");
            _httpClient.DefaultRequestHeaders.Add("X-Ordenamiento-Tipo", "ASC");
            _httpClient.DefaultRequestHeaders.Add("X-Pagina-Numero", "1");
            _httpClient.DefaultRequestHeaders.Add("X-Pagina-Elementos", "1500");

            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7149/api/visitas?periodo=2025");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var visitas = JsonSerializer.Deserialize<List<Visita>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return visitas;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener visitas: {ex.Message}");
                return null;
            }
        }

        static async Task<List<PersonaMoral>?> ObtenerPersonaMoral(int clavePes, int idSector)
        {
            var url = $"https://localhost:7001/api/v1/personas-morales?Id={clavePes}&SubSectores={idSector}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var personas = JsonSerializer.Deserialize<List<PersonaMoral>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return personas;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener persona moral (Id: {clavePes}, SubSector: {idSector}): {ex.Message}");
                return null;
            }
        }
    }

    // Clases para mapear las respuestas
    public class Visita
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombreEntidad { get; set; } = string.Empty;
        public int IdEntidad { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int ClavePes { get; set; }
        public int AreaId { get; set; }
        public int IdSector { get; set; }
        public string Sector { get; set; } = string.Empty;
        public int Periodo { get; set; }
        public string Participacion { get; set; } = string.Empty;
    }

    public class PersonaMoral
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public int SectorId { get; set; }
        public string SubSector { get; set; } = string.Empty;
        public int SubSectorId { get; set; }
        public string Vicepresidencia { get; set; } = string.Empty;
        public int VicepresidenciaId { get; set; }
        public string DireccionGeneral { get; set; } = string.Empty;
        public int DireccionGeneralId { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
