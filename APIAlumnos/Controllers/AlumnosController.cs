using APIAlumnos.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController:ControllerBase//Para las api web se utiliza contollerbase para las MVC controller
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
                return Ok(await alumnosRepositorio.DameAlumnos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error obteniendo los datos");
            }
        }
    }
}
