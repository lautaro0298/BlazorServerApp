
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using LibreriaClases;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace BlazorServer.Servicios
{
    public class ServicioAlumnos : IServicioAlumnos
    {
        private readonly HttpClient httpClient;

        public ServicioAlumnos(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Alumno>> DameAlumnos()
        {
            return await httpClient.GetFromJsonAsync<Alumno[]>("api/Alumnos");
        }
        public async Task<Alumno> DameAlumnos(int id)
        {
            return await httpClient.GetFromJsonAsync<Alumno>("api/Alumnos/id:int?id=" + id.ToString());
        }
        public async Task<Alumno> CrearAlumno(Alumno alumno)
        {
            try
            {
                var json = JsonSerializer.Serialize(alumno);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Alumnos", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Alumno alumnoCreado = JsonSerializer.Deserialize<Alumno>(responseBody);
                return alumnoCreado;
            }
            catch (HttpRequestException ex)
            {
                // Manejo de la excepción, puedes imprimir el mensaje o loggearlo
                Console.WriteLine($"Error de solicitud HTTP: {ex.Message}");
                return null;

            }
            catch (Exception ex)
            {
                // Otras excepciones
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return null;
            }
        }

        public async Task<Alumno> Modificar(int id, Alumno alumno)
        {
            try
            {
                var json = JsonSerializer.Serialize(alumno);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync("api/Alumnos/id:int?id=" + id, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Alumno alumnoCreado = JsonSerializer.Deserialize<Alumno>(responseBody);
                return alumnoCreado;
            }
            catch (HttpRequestException ex)
            {
                // Manejo de la excepción, puedes imprimir el mensaje o loggearlo
                Console.WriteLine($"Error de solicitud HTTP: {ex.Message}");
                return null;

            }
            catch (Exception ex)
            {
                // Otras excepciones
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> BorrarAlumno(int id)
        {
      
            var response=await httpClient.DeleteAsync($"/api/Alumnos/id:int?id={id}");
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            else
                return false;

        }
        public async Task<Alumno> CursosInscritosAlumno(int id)
        {
            return await httpClient.GetFromJsonAsync<Alumno>("API/Alumnos/CursosAlumno/" + id.ToString());
        }

    }
}
