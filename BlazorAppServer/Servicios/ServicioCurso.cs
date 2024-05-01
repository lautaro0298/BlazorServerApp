
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LibreriaClases;
using Microsoft.AspNetCore.Components;

namespace BlazorServer.Servicios
{
    public class ServicioCurso:IServicioCursos
    {
        private readonly HttpClient httpClient;

        public ServicioCurso(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Curso>> DameCursos(int idalumno)
        {
          
            return await httpClient.GetFromJsonAsync<Curso[]>("api/Curso/AlumnosCursos?id=" + idalumno.ToString());
        }
    }
}
