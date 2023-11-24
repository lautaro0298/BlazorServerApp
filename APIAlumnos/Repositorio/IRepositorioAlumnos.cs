using LibreriaClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAlumnos.Repositorio
{
    public interface IRepositorioAlumnos
    {
         Task<bool> AltaAlumno(Alumno Alumno);

         Task<IEnumerable<Alumno>> DameAlumnos();

         Task<Alumno> DameAlumno(int id);

         Task<bool> ModificarAlumno(Alumno Alumno);

         Task<bool> BorrarAlumno(int id);
    }
}
