using BlazorServer.Servicios;
using LibreriaClases;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class DetalleAlumnoBase: ComponentBase
    {
        [Inject]
        public IServicioAlumnos ServicioAlumnos { get; set; }
        [Parameter]
        public string Id { get; set; }
        public Alumno alumno { get; set; } = new Alumno();


        protected override async Task OnInitializedAsync()
        {
            alumno = (await ServicioAlumnos.DameAlumnos(Convert.ToInt32(Id)));
         
               
        }
    }
}
