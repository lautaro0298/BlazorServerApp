using LibreriaClases;

namespace APIAlumnos.Repositorio
{
    public interface IRepositorioCurso
    {
        Task<Curso> AltaCurso(Curso Curso);
        Task<IEnumerable<Curso>> DameCurso();
        Task<IEnumerable<Curso>> DameCurso(int idAlumno);

        Task<Curso> DameCursos(int id);
        Task<Curso> DameCursos(string email);
        Task<Curso> ModificarCurso(Curso Curso);
        Task<IEnumerable<Curso>> BuscarCurso(string texto);

        Task<Curso> BorrarCurso(int id);
    }
}
