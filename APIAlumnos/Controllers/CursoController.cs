using APIAlumnos.Repositorio;
using LibreriaClases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase//Para las api web se utiliza contollerbase para las MVC controller
    {
        private readonly IRepositorioCurso CursoRepositorio;

        public CursoController(IRepositorioCurso CursoRepositorio)
        {
            this.CursoRepositorio = CursoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult> DameCurso()
        {
            try
            {
                var resultado = await CursoRepositorio.DameCurso();
                if (resultado == null)
                {
                    return NotFound();
                }
                else
                    return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }

        [HttpGet("AlumnosCursos")]
        public async Task<ActionResult> DameCurso(int id)
        {
            try
            {
                return Ok(await CursoRepositorio.DameCurso(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpGet("{BuscarCurso}")]
        public async Task<ActionResult> DameCurso(string texto)
        {
            try
            {
                return Ok(await CursoRepositorio.BuscarCurso(texto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }

        [HttpPut("id:int")]
        public async Task<ActionResult<Curso>> ModificarCurso(int id, Curso Curso)
        {
            try
            {
                if (id != Curso.id)
                {
                    return BadRequest();
                }
                var CursoModificar = await CursoRepositorio.DameCursos(id);
                if (CursoModificar == null)
                {
                    return NotFound($"Curso con ={id} no encontrado");
                }
                return await CursoRepositorio.ModificarCurso(Curso);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpDelete("id:int")]
        public async Task<ActionResult<Curso>> EliminarCurso(int id, Curso Curso)
        {
            try
            {
                if (id != Curso.id)
                {
                    return BadRequest();
                }
                var CursoEliminar = await CursoRepositorio.DameCursos(id);
                if (CursoEliminar == null)
                {
                    return NotFound($"Curso con ={id} no encontrado");
                }
                return await CursoRepositorio.BorrarCurso(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Curso>> CrearCurso(Curso Curso)
        {
            try
            {
                if (Curso == null)
                {
                    return BadRequest();
                }
                var CursoAux = await CursoRepositorio.DameCursos(Curso.NombreCurso);
                if (CursoAux != null)
                {
                    ModelState.AddModelError("Nombre", "el nombre del curso ya esta esta en uso");
                    return BadRequest(ModelState);
                }
                var nuevoCurso = await CursoRepositorio.AltaCurso(Curso);
                return nuevoCurso;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
    }
}

