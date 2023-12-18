using APIAlumnos.Datos;
using APIAlumnos.Repositorio;

using LibreriaClases;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APICurso.Repositorio
{
    public class RepositiorioCurso : IRepositorioCurso
    {
        private string CadenaConexion;

        public RepositiorioCurso(AccesoDatos cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task<Curso> AltaCurso(Curso Curso)
        {
            Curso CursoCreado = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioAltaCurso";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@nombre", SqlDbType.VarChar, 500).Value = Curso.NombreCurso;
                reader = await Comm.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    CursoCreado = await DameCursos(Convert.ToInt32(reader["idCurso"]));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }

            return CursoCreado;

        }

        public async Task<Curso> BorrarCurso(int id)
        {
            Curso CursoBorrado = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();

            SqlDataReader reader = null;
            try
            {

                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioMarcaBaja";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("idCurso", SqlDbType.Int).Value = id;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if (await reader.ReadAsync())
                {
                    CursoBorrado = await DameCursos(Convert.ToInt32(reader["idCurso"]));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }



            return CursoBorrado;
        }

        public async Task<Curso> DameCursos(int id)
        {
            Curso Curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();

            SqlDataReader reader = null;
            try
            {

                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameCurso";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("id", SqlDbType.Int).Value = id;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if (await reader.ReadAsync())
                {
                    Curso = new Curso();
                    Curso.id = reader.GetInt32(reader.GetOrdinal("id"));
                    Curso.NombreCurso = reader.GetString(reader.GetOrdinal("nombre"));
                

                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }



            return Curso;
        }


        public async Task<IEnumerable<Curso>> DameCursos()
        {
            List<Curso> lista = new List<Curso>();
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameCurso";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while (await reader.ReadAsync())
                {
                    Curso alu = new Curso();
                    alu.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alu.NombreCurso = reader.GetString(reader.GetOrdinal("nombre"));


                    lista.Add(alu);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
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
        public async Task<Curso> ModificarCurso(Curso Curso)
        {
            Curso CursoModificado = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioAltaCurso";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idCurso", SqlDbType.Int).Value = Curso.id;
                Comm.Parameters.Add("@nombre", SqlDbType.VarChar, 500).Value = Curso.NombreCurso;
              
                reader = await Comm.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    CursoModificado = await DameCursos(Convert.ToInt32(reader["idCurso"]));
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }

            return CursoModificado;

        }

        public async Task<Curso> DameCursos(string email)
        {
            Curso Curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();

            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioDameCurso";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("email", SqlDbType.VarChar).Value = email;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if (await reader.ReadAsync())
                {
                    Curso = new Curso();
                    Curso.id = reader.GetInt32(reader.GetOrdinal("id"));
                    Curso.NombreCurso = reader.GetString(reader.GetOrdinal("nombre"));


                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    await reader.CloseAsync();

                await Comm.DisposeAsync();
                await sqlConexion.CloseAsync();
                await Comm.DisposeAsync();
            }



            return Curso;
        }

        public async Task<IEnumerable<Curso>> BuscarCurso(string texto)
        {
            List<Curso> lista = new List<Curso>();
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.UsuarioBuscarCurso";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@texto", SqlDbType.VarChar, 500).Value = texto;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while (await reader.ReadAsync())
                {
                    Curso alu = new Curso();
                    alu.id = reader.GetInt32(reader.GetOrdinal("id"));
                    alu.NombreCurso = reader.GetString(reader.GetOrdinal("nombre"));
                   

                    lista.Add(alu);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
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
