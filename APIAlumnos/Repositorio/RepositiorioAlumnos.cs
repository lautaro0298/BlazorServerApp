using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAlumnos.Datos;
using Microsoft.Data.SqlClient;
using LibreriaClases;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.PortableExecutable;

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

        public async Task<Alumno> AltaAlumno(Alumno Alumno)
        {
            Alumno alumnoCreado = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioAltaAlumno";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@nombre", SqlDbType.VarChar, 500).Value = Alumno.nombre;
                Comm.Parameters.Add("@email", SqlDbType.VarChar, 500).Value = Alumno.email;
                Comm.Parameters.Add("@foto", SqlDbType.VarChar, 500).Value = Alumno.foto;
                Comm.Parameters.Add("@fechaAlta", SqlDbType.DateTime).Value = Alumno.fechaAlta;
                reader = await Comm.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    alumnoCreado = await DameAlumnos(Convert.ToInt32(reader["idAlumno"]));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros alumno " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }

            return alumnoCreado;

        }

        public async Task<Alumno> BorrarAlumno(int id)
        {
            Alumno alumnoBorrado = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();

            SqlDataReader reader = null;
            try
            {
                
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioMarcaBaja";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("idAlumno", SqlDbType.Int).Value = id;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if (await reader.ReadAsync())
                {
                    alumnoBorrado = await DameAlumnos(Convert.ToInt32(reader["idAlumno"]));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros alumno " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }



            return alumnoBorrado;
        }

        public async Task<Alumno> DameAlumnos(int id)
        {
           Alumno alumno = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();
     
            SqlDataReader reader = null;
            try
            {

                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameAlumnos";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("id",SqlDbType.Int).Value=id;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if (await reader.ReadAsync())
                {
                    alumno = new Alumno();
                    alumno.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alumno.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    alumno.email = reader.GetString(reader.GetOrdinal("email"));
                    alumno.foto = reader.GetString(reader.GetOrdinal("foto"));
                    alumno.fechaAlta = reader.GetDateTime(reader.GetOrdinal("fechaAlta"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fechaBaja")))     //Se ha utilizado el método GetInt32(), GetString(), GetDateTime() y IsDBNull() en lugar de Convert.ToInt32(), ToString(), Convert.ToDateTime() y System.DBNull.Value respectivamente para mejorar la legibilidad del código.
                        alumno.fechaBaja = reader.GetDateTime(reader.GetOrdinal("fechaBaja"));

                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros alumno " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }

        

            return alumno;
        }
        //Esto es ADO.NET Se puede crear las consultas directamente en el Codigo pero no es recomendable lo mejor es accedar a procedimiento almacenados ya que tienen toda una estructura de proteccion
        /*   
         *    public async Task<IEnumerable<Alumno>> DameAlumnos(){
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
         */
        
        
        public async Task<IEnumerable<Alumno>> DameAlumnos() 
        {
            List<Alumno> lista = new List<Alumno>();
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                     //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameAlumnos";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while (await reader.ReadAsync())
                {
                    Alumno alu = new Alumno();
                    alu.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alu.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    alu.email = reader.GetString(reader.GetOrdinal("email"));
                    alu.foto = reader.GetString(reader.GetOrdinal("foto"));
                    alu.fechaAlta = reader.GetDateTime(reader.GetOrdinal("fechaAlta"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fechaBaja")))     //Se ha utilizado el método GetInt32(), GetString(), GetDateTime() y IsDBNull() en lugar de Convert.ToInt32(), ToString(), Convert.ToDateTime() y System.DBNull.Value respectivamente para mejorar la legibilidad del código.
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
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }


            return lista;
        }
        public async Task<Alumno> ModificarAlumno(Alumno alumno)
        {
            Alumno alumnoModificado = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioAltaAlumno";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idAlumno", SqlDbType.Int).Value = alumno.id;
                Comm.Parameters.Add("@nombre", SqlDbType.VarChar, 500).Value = alumno.nombre;
                Comm.Parameters.Add("@email", SqlDbType.VarChar, 500).Value = alumno.email;
                Comm.Parameters.Add("@foto", SqlDbType.VarChar, 500).Value = alumno.foto;
                Comm.Parameters.Add("@fechaAlta", SqlDbType.DateTime).Value = alumno.fechaAlta;
               
                if (alumno.fechaBaja!=null)
                {
                    Comm.Parameters.Add("@fechaBaja", SqlDbType.DateTime).Value = alumno.fechaBaja;
                }
                reader = await Comm.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    alumnoModificado= await DameAlumnos(Convert.ToInt32(reader["idAlumno"]));
                }
               
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros alumno " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }

            return alumnoModificado;

        }

        public async Task<Alumno> DameAlumnos(string email)
        {
            Alumno alumno = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();

            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameAlumnos";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("email", SqlDbType.VarChar).Value = email;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if (await reader.ReadAsync())
                {
                    alumno = new Alumno();
                    alumno.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alumno.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    alumno.email = reader.GetString(reader.GetOrdinal("email"));
                    alumno.foto = reader.GetString(reader.GetOrdinal("foto"));
                    alumno.fechaAlta = reader.GetDateTime(reader.GetOrdinal("fechaAlta"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fechaBaja")))     //Se ha utilizado el método GetInt32(), GetString(), GetDateTime() y IsDBNull() en lugar de Convert.ToInt32(), ToString(), Convert.ToDateTime() y System.DBNull.Value respectivamente para mejorar la legibilidad del código.
                        alumno.fechaBaja = reader.GetDateTime(reader.GetOrdinal("fechaBaja"));

                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros alumno " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }



            return alumno;
        }

        public async Task<IEnumerable<Alumno>> BuscarAlumnos(string texto)
        {
            List<Alumno> lista = new List<Alumno>();
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioBuscarAlumnos";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@texto", SqlDbType.VarChar, 500).Value = texto;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while (await reader.ReadAsync())
                {
                    Alumno alu = new Alumno();
                    alu.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alu.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    alu.email = reader.GetString(reader.GetOrdinal("email"));
                    alu.foto = reader.GetString(reader.GetOrdinal("foto"));
                    alu.fechaAlta = reader.GetDateTime(reader.GetOrdinal("fechaAlta"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fechaBaja")))     //Se ha utilizado el método GetInt32(), GetString(), GetDateTime() y IsDBNull() en lugar de Convert.ToInt32(), ToString(), Convert.ToDateTime() y System.DBNull.Value respectivamente para mejorar la legibilidad del código.
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
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }


            return lista;
        }
    }
}

   