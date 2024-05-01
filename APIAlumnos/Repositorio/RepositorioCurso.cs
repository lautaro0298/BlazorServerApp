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
            int idCursoCreado=-1;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.CursoAlta";
                Comm.CommandType = CommandType.StoredProcedure;
                int cout = 0;
                while (cout < Curso.ListaPrecio.Count) { 
                Comm.Parameters.Add("@NombreCurso", SqlDbType.VarChar, 500).Value = Curso.NombreCurso;
                Comm.Parameters.Add("@Costo", SqlDbType.Money).Value = Curso.ListaPrecio.FirstOrDefault().Costo;
                Comm.Parameters.Add("@fechaAlta", SqlDbType.DateTime).Value = Curso.ListaPrecio.FirstOrDefault().fechaAlta;
                Comm.Parameters.Add("@fechaBaja", SqlDbType.DateTime).Value = Curso.ListaPrecio.FirstOrDefault().fechaBaja;
                    idCursoCreado = Convert.ToInt32(await Comm.ExecuteScalarAsync());
                cout++;
                }
                if (idCursoCreado != -1)
                {
                    CursoCreado = await DameCursos(idCursoCreado);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros Curso " + ex.Message);
            }
            finally
            {
               
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
                Comm.CommandText = "dbo.CursoBorrar";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("idCurso", SqlDbType.Int).Value = id;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if ( reader.Read())
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
            List<Curso> lista = new List<Curso>();
            Curso curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.CursoDame";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@id", SqlDbType.Int).Value = id;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while (reader.Read())
                {
                    if (curso == null || curso.id != Convert.ToInt32(reader["idCurso"]))
                    {
                        if (curso != null)
                            return curso;
                        Curso alu = new Curso();
                        alu.id = Convert.ToInt32(reader["idCurso"]);
                        alu.NombreCurso = reader["NombreCurso"].ToString();
                        
                        alu.ListaPrecio = new List<Precio>();
                        curso = alu;
                    }
                    Precio aux = new Precio();
                    
                    aux.id = Convert.ToInt32(reader["idPrecio"]);
                    aux.Costo = Convert.ToDouble(reader["Costo"]);
                    aux.fechaAlta = Convert.ToDateTime(reader["fechaAlta"]);
                    aux.fechaBaja = Convert.ToDateTime(reader["fechaBaja"]);
                    curso.ListaPrecio.Add(aux);

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


            if (curso != null)
            { return curso; }
            else return null;


        }
        public async Task<IEnumerable<Curso>> DameCurso(int idAlumno)
        {
            List<Curso> lista = new List<Curso>();
            Curso curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.CursoDame";
                Comm.CommandType = CommandType.StoredProcedure;
                if (idAlumno != -1)
                {
                    Comm.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;
                }
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while (reader.Read())
                {
                    if (curso == null || curso.id != Convert.ToInt32(reader["idCurso"]))
                    {
                        if (curso != null)
                            lista.Add(curso);
                        curso = new Curso();

                        curso.id = reader.GetInt32(reader.GetOrdinal("idCurso"));
                        curso.NombreCurso = reader.GetString(reader.GetOrdinal("NombreCurso"));
                        curso.ListaPrecio = new List<Precio>();
                    }
                    Precio aux = new Precio();
                    aux.id = Convert.ToInt32(reader["idPrecio"]);
                    aux.Costo = Convert.ToDouble(reader["Costo"]);
                    aux.fechaAlta = Convert.ToDateTime(reader["fechaAlta"]);
                    aux.fechaBaja = Convert.ToDateTime(reader["fechaBaja"]);
                    curso.ListaPrecio.Add(aux);
                }
                if (curso != null)
                {
                    lista.Add(curso);
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

        public async Task<IEnumerable<Curso>> DameCurso()
        {
            List<Curso> lista = new List<Curso>();
            Curso curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.CursoDame";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while ( reader.Read())
                {
                    if (curso == null || curso.id != Convert.ToInt32(reader["idCurso"]))
                    { 
                     if(curso!=null)
                            lista.Add(curso);
                        curso = new Curso();

                    curso.id = reader.GetInt32(reader.GetOrdinal("idCurso"));
                    curso.NombreCurso = reader.GetString(reader.GetOrdinal("NombreCurso"));
                     curso.ListaPrecio=new List<Precio>();
                    }
                    Precio aux =new Precio();
                    aux.id = Convert.ToInt32(reader["idPrecio"]);
                    aux.Costo = Convert.ToDouble(reader["Costo"]);
                    aux.fechaAlta = Convert.ToDateTime(reader["fechaAlta"]);
                    aux.fechaBaja = Convert.ToDateTime(reader["fechaBaja"]);
                    curso.ListaPrecio.Add(aux);
                }
                if (curso != null)
                {
                    lista.Add(curso);
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
                Comm.CommandText = "dbo.CursoModificar";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idCurso", SqlDbType.Int).Value = Curso.id;
                Comm.Parameters.Add("@idPrecio", SqlDbType.Int).Value = Curso.ListaPrecio.FirstOrDefault()?.id;
                Comm.Parameters.Add("@NombreCurso", SqlDbType.VarChar, 500).Value = Curso.NombreCurso;
                Comm.Parameters.Add("@Costo", SqlDbType.Money).Value = Curso.ListaPrecio.FirstOrDefault()?.Costo;
                Comm.Parameters.Add("@fechaAlta", SqlDbType.DateTime).Value = Curso.ListaPrecio.FirstOrDefault()?.fechaAlta;
                Comm.Parameters.Add("@fechaBaja", SqlDbType.DateTime).Value = Curso.ListaPrecio.FirstOrDefault()?.fechaBaja;
                reader = await Comm.ExecuteReaderAsync();
                if ( reader.Read())
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

        public async Task<Curso> DameCursos(string NombreCurso)
        {
            Curso Curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();

            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.CursoDame";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("NombreCurso", SqlDbType.VarChar).Value = NombreCurso;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                if ( reader.Read())
                {
                    if (Curso == null)
                    {
                        Curso = new Curso();
                        Curso.id = reader.GetInt32(reader.GetOrdinal("id"));
                        Curso.NombreCurso = reader.GetString(reader.GetOrdinal("NombreCurso"));

                    }


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
            Curso curso = null;
            await using SqlConnection sqlConexion = conexion();
            await using SqlCommand Comm = sqlConexion.CreateCommand();//Se ha agregado la palabra clave using a la definición de la conexión SQL y el comando SQL para garantizar que se liberen los recursos de manera adecuada después de su uso.
                                                                      //  Estos cambios se han realizado para mejorar la eficiencia y la legibilidad del código en.NET 7.
                                                                      //   En.NET 7, la creación de un objeto SqlCommand se puede realizar directamente en la definición de la conexión SQL mediante el uso de la palabra clave using. En la versión actualizada del código, se ha utilizado la palabra clave using para definir la conexión SQL y el comando SQL.
                                                                      //Esto garantiza que los recursos se liberen de manera adecuada después de su uso y mejora la legibilidad del código
            SqlDataReader reader = null;
            try
            {
                await sqlConexion.OpenAsync();
                Comm.CommandText = "dbo.CursoDame";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@NombreCurso", SqlDbType.VarChar, 500).Value = texto;
                reader = await Comm.ExecuteReaderAsync();//Se ha agregado la palabra clave await a las llamadas a los métodos OpenAsync() y ExecuteReaderAsync() para indicar que son operaciones asincrónicas
                while ( reader.Read())
                {
                    if (curso == null|| curso.id != Convert.ToInt32(reader["idCurso"]))
                        {
                        if (curso != null)
                            lista.Add(curso);
                        Curso alu = new Curso();
                       
                        alu.id = Convert.ToInt32(reader["idCurso"]);
                        alu.NombreCurso = reader["NombreCurso"].ToString();
                        alu.ListaPrecio = new List<Precio>();
                        curso = alu;
                        }
                        Precio aux = new Precio();
                        aux.id = Convert.ToInt32(reader["idPrecio"]);
                        aux.Costo = Convert.ToDouble(reader["Costo"]);
                        aux.fechaAlta = Convert.ToDateTime(reader["fechaAlta"]);
                        aux.fechaBaja = Convert.ToDateTime(reader["fechaBaja"]);
                        curso.ListaPrecio.Add(aux);
                    
                }
                if (curso != null)
                    lista.Add(curso);
             
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
