using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAlumnos.Datos
{
    public class AccesoDatos
    {
        private string cadenaConexionSql;
        public string CadenaConexionSQL { get => cadenaConexionSql; }
        public AccesoDatos(string ConexionSql)
        {
            cadenaConexionSql = ConexionSql;//esto sirve por si necesitara añadir mas cadenas de conexion simplemente se añade la otra 
        }
    }
}
