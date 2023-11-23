using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases
{
    public class Alumno
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime? fechaBaja { get; set; }
        public List<Curso> ListaCurso { get; set; }
    }
}
