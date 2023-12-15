using LibreriaClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAlumnos.Repositorio
{
    public interface IRepositorioAlumnos
    {
         Task<Alumno> AltaAlumno(Alumno Alumno);

         Task<IEnumerable<Alumno>> DameAlumnos();

        Task<Alumno> DameAlumnos(int id);
        Task<Alumno> DameAlumnos(string email);
        Task<Alumno> ModificarAlumno(Alumno Alumno);

         Task<Alumno> BorrarAlumno(int id);
        Task<IEnumerable<Alumno>>BuscarAlumnos(string texto);

    }
}
