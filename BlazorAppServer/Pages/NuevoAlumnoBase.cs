using BlazorServer.Servicios;
using LibreriaClases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class NuevoAlumnoBase : ComponentBase
    {
        [Inject]
        public IServicioAlumnos ServicioAlumnos { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Alumno alumno = new Alumno();
        public IBrowserFile file;

        public async Task HandleValidSubmit()
        {
            Console.WriteLine("OnValidSubmit");
            await Guardar();
        }

        protected async Task Guardar()
        {
            try
            {
                alumno.fechaAlta = DateTime.Now;

                if (alumno.nombre != null && alumno.email != null && file != null)
                {
                    var nombreFichero = "images/" + Guid.NewGuid() + ".jpg";
                    var ms = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(ms);
                    using (var fs = new FileStream("wwwroot/" + nombreFichero, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(fs);
                    }

                    alumno.foto = nombreFichero;
                    alumno = await ServicioAlumnos.CrearAlumno(alumno);
                    NavigationManager.NavigateTo("/listaAlumnos");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
        }

        protected void Cancelar()
        {
            NavigationManager.NavigateTo("/listaAlumnos");
        }


        public void HandleFileSelected(InputFileChangeEventArgs e)
        {
            file = e.File;
            // Puedes realizar más validaciones o procesamientos aquí si es necesario
        }
    }
}
