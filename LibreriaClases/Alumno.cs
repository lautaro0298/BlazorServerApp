using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases
{
    public class Alumno
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage ="El campo email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato de email incorrecto")]
        public string email { get; set; }
        [Required(ErrorMessage = "El campo foto es obligatorio")]
        public string foto { get; set; }
        [Required(ErrorMessage = "El campo mail es obligatorio")]
        public DateTime fechaAlta { get; set; }
        public DateTime? fechaBaja { get; set; }
        public List<Curso>? ListaCurso { get; set; }
    }
}
