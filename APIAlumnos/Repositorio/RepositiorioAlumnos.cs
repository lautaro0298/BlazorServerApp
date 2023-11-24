using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAlumnos.Datos;
using Microsoft.Data.SqlClient;
using LibreriaClases;
using System.Data;

namespace APIAlumnos.Repositorio
{
    public class RepositiorioAlumnos : IRepositorioAlumnos
    {
        private string CadenaConexion;

        public RepositiorioAlumnos(AccesoDatos cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public Task<bool> AltaAlumno(Alumno Alumno)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BorrarAlumno(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Alumno> DameAlumno(int id)
        {
            throw new NotImplementedException();
        }
        //Esto es ADO.NET Se puede crear las consultas directamente en el Codigo pero no es recomendable lo mejor es accedar a procedimiento almacenados ya que tienen toda una estructura de proteccion
        public async Task<IEnumerable<Alumno>> DameAlumnos()
        {
            List<Alumno> lista = new List<Alumno>();
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            SqlDataReader reader = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuarioDameAlumnos";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Alumno alu = new Alumno();
                    alu.id = Convert.ToInt32(reader["id"]);
                    alu.nombre = reader["nombre"].ToString();
                    alu.email = reader["nmail"].ToString();
                    alu.foto = reader["foto"].ToString();
                    alu.fechaAlta = Convert.ToDateTime(reader["fechaAlta"].ToString());
                    if (reader["fechaBaja"] != System.DBNull.Value)
                        alu.fechaBaja = Convert.ToDateTime(reader["fechaBaja"].ToString());

                    lista.Add(alu);
                }


            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros alumno " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return lista;
        }

        public Task<bool> ModificarAlumno(Alumno Alumno)
        {
            throw new NotImplementedException();
        }
    }
}

   