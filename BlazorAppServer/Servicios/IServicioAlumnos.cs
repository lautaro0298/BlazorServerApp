using LibreriaClases;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Servicios
{
    public interface IServicioAlumnos
    {
         Task<IEnumerable<Alumno>> DameAlumnos();
        Task<Alumno> DameAlumnos(int id);
        Task<Alumno> CrearAlumno(Alumno alumno);
        Task<Alumno> Modificar(int id, Alumno alumno);
        Task<bool> BorrarAlumno(int id);
        Task<Alumno> CursosInscritosAlumno(int id);
    }
}
