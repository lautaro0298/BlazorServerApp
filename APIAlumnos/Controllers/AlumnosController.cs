using APIAlumnos.Repositorio;
using LibreriaClases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase//Para las api web se utiliza contollerbase para las MVC controller
    {
        private readonly IRepositorioAlumnos alumnosRepositorio;

        public AlumnosController(IRepositorioAlumnos alumnosRepositorio)
        {
            this.alumnosRepositorio = alumnosRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult> DameAlumnos()
        {
            try
            {
                var resultado = await alumnosRepositorio.DameAlumnos();
                if (resultado == null)
                {
                    return NotFound();
                } else
                    return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }

        [HttpGet("id:int")]
        public async Task<ActionResult> DameAlumnos(int id)
        {
            try
            {
                return Ok(await alumnosRepositorio.DameAlumnos(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpGet("{BuscarAlumnos}")]
        public async Task<ActionResult> DameAlumnos(string texto)
        {
            try
            {
                return Ok(await alumnosRepositorio.BuscarAlumnos(texto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }

        [HttpPut("id:int")]
        public async Task<ActionResult<Alumno>> ModificarAlumno(int id,Alumno alumno)
        {
            try
            {
                if(id != alumno.id)
                {
                    return BadRequest();
                }
              var alumnoModificar= await alumnosRepositorio.DameAlumnos(id);
              if(alumnoModificar == null)
                {
                    return NotFound($"Alumno con ={id} no encontrado" );
                }
                return await alumnosRepositorio.ModificarAlumno(alumno);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpDelete("id:int")]
        public async Task<ActionResult<Alumno>> EliminarAlumno(int id)
        {
            try
            {
                
                var alumnoEliminar = await alumnosRepositorio.DameAlumnos(id);
                if (alumnoEliminar == null)
                {
                    return NotFound($"Alumno con ={id} no encontrado");
                }
                return await alumnosRepositorio.BorrarAlumno(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Alumno>> CrearAlumno(Alumno alumno)
        {
            try
            {
                if (alumno == null)
                {
                    return BadRequest();
                }
                var alumnoAux = await alumnosRepositorio.DameAlumnos(alumno.email);
                if(alumnoAux != null)
                {
                    ModelState.AddModelError("email", "el email ya esta esta en uso");
                    return BadRequest(ModelState) ;
                }
                var nuevoAlumno = await alumnosRepositorio.AltaAlumno(alumno);
                return nuevoAlumno;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
        [HttpPost("InscribirAlumno/{idCurso}")]
        public async Task<ActionResult<Alumno>> InscribirAlumnoCurso([FromBody] Alumno alumno, int idCurso)
        {
            try
            {
                var alumnoValidar = await alumnosRepositorio.DameAlumnos(alumno.id);

                if (alumnoValidar == null)
                    return NotFound($"Alumno no encontrado");

                return await alumnosRepositorio.inscribirAlumnoCurso(alumno, idCurso);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inscribiendo alumno en curso");
            }
        }

        [HttpGet("CursosAlumno/{idAlumno}")]
        public async Task<ActionResult<Alumno>> AlumnoCursos(int idAlumno)
        {
            try
            {
                Alumno respuesta = null;
                var alumnoValidar = await alumnosRepositorio.DameAlumnos(idAlumno);

                if (alumnoValidar == null)
                    return NotFound($"Alumno no encontrado");
                respuesta = await alumnosRepositorio.AlumnoCurso(idAlumno);
                if (respuesta == null)
                {
                    respuesta = alumnoValidar
                        ;
                }


                return respuesta;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo cursos alumno");
            }
        }
    }
}


