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
        Task<bool> ModificarAlumno(Alumno Alumno);

         Task<bool> BorrarAlumno(int id);
    }
}
