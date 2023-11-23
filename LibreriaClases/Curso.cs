using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases
{
   public  class Curso
    {

        public int id {  get; set; }
        public string NombreCurso { get; set; }
        public List<Precio> ListaPrecio { get; set; }
        
    }
}
