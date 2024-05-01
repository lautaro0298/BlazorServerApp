using LibreriaClases;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Servicios
{
    public interface IServicioCursos
    {
        Task<IEnumerable<Curso>> DameCursos(int idalumno);
    }
}
