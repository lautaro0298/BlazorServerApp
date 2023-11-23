using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriaClases;

namespace BlazorServer.Pages
{
    public class ListaAlumnosBase: ComponentBase
    {
        public IEnumerable<Alumno> Alumnos { get; set; }

        protected override Task OnInitializedAsync()
        {
            CargarAlumnos();
            return base.OnInitializedAsync();
        }

        private void CargarAlumnos()
        {

            Precio precioBlazor = new Precio();
            precioBlazor.id = 1;
            precioBlazor.Costo = 19.99;
            precioBlazor.fechaAlta = DateTime.Now;
            precioBlazor.fechaBaja = DateTime.Now.AddDays(3); 

            Curso cursoBlazor = new Curso();
            cursoBlazor.id = 1;
            cursoBlazor.NombreCurso = "Curso Blazor";
            cursoBlazor.ListaPrecio = new List<Precio>();
            cursoBlazor.ListaPrecio?.Add(precioBlazor);

            Alumno alumno1 = new Alumno
            {
                id = 1,
                nombre = "Jap Software",
                email = "Mail@pruebamail.es",
                foto = "images/Alumno1.jpg",
                ListaCurso = new List<Curso>(),
                fechaAlta = DateTime.Now,
                fechaBaja = null,

            };

            Alumno alumno2 = new Alumno
            {
                id = 2,
                nombre = "Jap Software 2",
                email = "Mail2@pruebamail.es",
                foto = "images/Alumno2.jpg",
                ListaCurso = new List<Curso>(),
                fechaAlta = DateTime.Now,
                fechaBaja = null,

            };

            Alumno alumno3 = new Alumno
            {
                id = 3,
                nombre = "Jap Software 3",
                email = "Mail3@pruebamail.es",
                foto = "images/ChicaCodigo65.jpg",
                ListaCurso = new List<Curso>(),
                fechaAlta = DateTime.Now,
                fechaBaja = null,

            };

            alumno1.ListaCurso.Add(cursoBlazor);
            alumno2.ListaCurso.Add(cursoBlazor);
            alumno3.ListaCurso.Add(cursoBlazor);

            Alumnos= new List<Alumno> { alumno1,alumno2,alumno3 };
        }
    }

}
