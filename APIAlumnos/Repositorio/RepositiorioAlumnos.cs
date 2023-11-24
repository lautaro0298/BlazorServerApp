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
        /*   {
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
        
         En la versión actualizada del código, se han realizado los siguientes cambios:

Se ha agregado la palabra clave async a la definición del método DameAlumnos() para indicar que es un método asincrónico.
Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas.
Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
Se ha utilizado el método GetInt32(), GetString(), GetDateTime() y IsDBNull() en lugar de Convert.ToInt32(), ToString(), Convert.ToDateTime() y System.DBNull.Value respectivamente para mejorar la legibilidad del código.
Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en .NET 7.
 En .NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
 Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código 
         */
        {
            List<Alumno> lista = new List<Alumno>();
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameAlumnos";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Alumno alu = new Alumno();
                    alu.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alu.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    alu.email = reader.GetString(reader.GetOrdinal("nmail"));
                    alu.foto = reader.GetString(reader.GetOrdinal("foto"));
                    alu.fechaAlta = reader.GetDateTime(reader.GetOrdinal("fechaAlta"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fechaBaja")))
                        alu.fechaBaja = reader.GetDateTime(reader.GetOrdinal("fechaBaja"));

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
                await sqlConexion.CloseAsync();
            }

            return lista;
        }
        public Task<bool> ModificarAlumno(Alumno Alumno)
        {
            throw new NotImplementedException();
        }
    }
}

   