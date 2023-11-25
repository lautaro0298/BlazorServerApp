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



        [HttpPost]
        public async Task<ActionResult<Alumno>> CrearAlumno(Alumno alumno)
        {
            try
            {
                if (alumno == null)
                {
                    return BadRequest();
                }
                var nuevoAlumno = await alumnosRepositorio.AltaAlumno(alumno);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo los datos");
            }
        }
    }
}


