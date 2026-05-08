using LoadVIS.Database;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LoadVIS
{
    public class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            PtsAccionesContext dbContext = new PtsAccionesContext();

            await dbContext.Database.BeginTransactionAsync();

            try
            {
                var visitas = await ObtenerVisitas();
                var personasMoralesNotFound = new List<Visita>();

                if (visitas != null && visitas.Any())
                {
                    var accionesSupervision = new List<AccionSupervision>();

                    int index = 1;

                    foreach (var visita in visitas)
                    {
                        Console.WriteLine($"{index}.- {visita.Id}");

                        var persona = await ObtenerPersonaMoral(visita.ClavePes, visita.IdSector);
                        if (persona != null && persona.Any())
                        {
                            ////personasMorales.AddRange(persona);
                            var firstPersona = persona.First();

                            string casfim = await ObtenerCasfim(visita.ClavePes, firstPersona.SubSectorId);

                            List<Kardex> kardexList = new List<Kardex>();

                            if (!string.IsNullOrEmpty(casfim))
                            {
                                kardexList = await ObtenerKardex(casfim);
                            }

                            Kardex cefer = kardexList.LastOrDefault();

                            AccionSupervision nuevaAccion = new AccionSupervision
                            {
                                IdEntidadExt = visita.ClavePes,
                                ClavePes = visita.ClavePes.ToString(),
                                Casfim = casfim ?? string.Empty,
                                DenominacionEntidad = firstPersona.RazonSocial,
                                NombreCortoEntidad = firstPersona.NombreCorto,
                                IdSectorExt = firstPersona.SectorId,
                                NombreSector = firstPersona.Sector,
                                IdSubsectorExt = firstPersona.SubSectorId,
                                NombreSubsector = firstPersona.SubSector,
                                IdVpExt = firstPersona.VicepresidenciaId,
                                NombreVp = firstPersona.Vicepresidencia,
                                ClaveVp = GenerarClave(firstPersona.Vicepresidencia),
                                IdDgExt = firstPersona.DireccionGeneralId,
                                NombreDg = firstPersona.DireccionGeneral,
                                ClaveDg = GenerarClave(firstPersona.DireccionGeneral),
                                IdTipoAccion = visita.Tipo.Equals("Ordinaria") ? 1 : 2,
                                CeferRegistro = cefer != null ? cefer.Calificacion : null,
                                CeferPeriodo = cefer != null ? cefer.Periodo : null,
                                FechaInicioPlan = visita.FechaInicio,
                                FechaFinPlan = visita.FechaFin,
                                FechaAlta = DateTime.Now,
                                UsuarioAlta = "ID004478",
                                IdAccion = visita.Id,
                                Habilitado = true,
                                Terminado = false,
                                IdEstado = 1
                            };

                            await dbContext.AccionSupervisions.AddAsync(nuevaAccion);
                            await dbContext.SaveChangesAsync();

                            EjecucionAccion nuevaEjecucion = new EjecucionAccion
                            {
                                IdAccion = nuevaAccion.IdAccion
                            };

                            await dbContext.EjecucionAccions.AddAsync(nuevaEjecucion);
                            await dbContext.SaveChangesAsync();

                            ObservacionRecomendacion nuevaObservacion = new ObservacionRecomendacion
                            {
                                IdAccion = nuevaAccion.IdAccion
                            };

                            await dbContext.ObservacionRecomendacions.AddAsync(nuevaObservacion);
                            await dbContext.SaveChangesAsync();

                            AccionMedidaCorrectiva nuevaMedida = new AccionMedidaCorrectiva
                            {
                                IdAccion = nuevaAccion.IdAccion
                            };

                            await dbContext.AccionMedidaCorrectivas.AddAsync(nuevaMedida);
                            await dbContext.SaveChangesAsync();

                            SancionControl nuevaSancion = new SancionControl
                            {
                                IdAccion = nuevaAccion.IdAccion
                            };

                            await dbContext.SancionControls.AddAsync(nuevaSancion);
                            await dbContext.SaveChangesAsync();

                            accionesSupervision.Add(nuevaAccion);
                        }
                        else
                        {
                            personasMoralesNotFound.Add(visita);
                        }

                        index++;
                    }

                }

                await dbContext.Database.CommitTransactionAsync();

                Console.WriteLine($"Se procesaron {visitas.Count} visitas");
                Console.WriteLine($"No se encontraron {personasMoralesNotFound.Count} personas morales");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await dbContext.Database.RollbackTransactionAsync();
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
                var response = await _httpClient.GetAsync("https://localhost:7149/api/visitas?periodo=2026");
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

        static async Task<string> ObtenerCasfim(int personaMoralId, int subSectorId)
        {
            var url = $"https://localhost:7001/api/v1/claves-dinamicas/valor?personaMoralId={personaMoralId}&subSectorId={subSectorId}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CasfimResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var casfim = result?.Casfim ?? string.Empty;

                // Validar que el valor no sea nulo o vacío
                if (string.IsNullOrEmpty(casfim))
                {
                    Console.WriteLine($"CASFIM vacío para personaMoralId={personaMoralId}, subSectorId={subSectorId}");
                    return string.Empty;
                }

                // Validar que solo contenga números
                if (!casfim.All(char.IsDigit))
                {
                    Console.WriteLine($"CASFIM inválido (contiene caracteres no numéricos): '{casfim}' para personaMoralId={personaMoralId}");
                    return string.Empty;
                }

                // Validar que no sea un número negativo o cero (opcional)
                if (int.TryParse(casfim, out int casfimNumber) && casfimNumber <= 0)
                {
                    Console.WriteLine($"CASFIM inválido (valor <= 0): '{casfim}' para personaMoralId={personaMoralId}");
                    return string.Empty;
                }

                // Formatear a 6 dígitos con ceros a la izquierda
                string casfimFormateado = casfimNumber.ToString("D6");
                Console.WriteLine($"CASFIM válido: '{casfim}' -> '{casfimFormateado}'");

                return casfimFormateado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener CASFIM (personaMoralId: {personaMoralId}, subSectorId: {subSectorId}): {ex.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al obtener CASFIM: {ex.Message}");
                return string.Empty;
            }
        }

        static async Task<List<Kardex>> ObtenerKardex(string casfim)
        {
            var token = Environment.GetEnvironmentVariable("AZURE_TOKEN");
            var url = $"https://localhost:54560/api/kardex?casfim={casfim}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var kardex = JsonSerializer.Deserialize<List<Kardex>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Ordenar ascendente por periodo
                return kardex?.OrderBy(k => k.Periodo).ToList() ?? new List<Kardex>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener kardex (CASFIM: {casfim}): {ex.Message}");
                return new List<Kardex>();
            }
        }

        static string GenerarClave(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // Palabras a omitir (solo cuando están en medio)
            var palabrasAOmitir = new HashSet<string> { "DE", "Y", "E", "DEL", "LA", "LAS", "LOS", "EL" };

            // Dividir el texto por espacios
            var palabras = texto.Trim().Split(new[] { ' ', '-', '_', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var clave = new List<string>();
            int totalPalabras = palabras.Length;

            for (int i = 0; i < totalPalabras; i++)
            {
                var palabra = palabras[i].ToUpper();

                // Si es la última palabra, incluirla siempre
                if (i == totalPalabras - 1)
                {
                    if (!string.IsNullOrEmpty(palabra))
                        clave.Add(char.ToUpper(palabras[i][0]).ToString());
                }
                // Si no es la última palabra, omitir si está en la lista de palabras a omitir
                else if (!palabrasAOmitir.Contains(palabra))
                {
                    if (!string.IsNullOrEmpty(palabra))
                        clave.Add(char.ToUpper(palabras[i][0]).ToString());
                }
            }

            return string.Concat(clave);
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

    public class CasfimResponse
    {
        public string Casfim { get; set; } = string.Empty;
    }

    public class Kardex
    {
        public string Periodo { get; set; } = string.Empty;
        public string Calificacion { get; set; } = string.Empty;
    }
}
