using LibreriaClases;

namespace APIAlumnos.Repositorio
{
    public interface IRepositorioCurso
    {
        Task<Curso> AltaCurso(Curso Curso);


        Task<Curso> DameCursos(int id);
        Task<Curso> DameCursos(string email);
        Task<Curso> ModificarCurso(Curso Curso);

        Task<Curso> BorrarCurso(int id);
    }
}
