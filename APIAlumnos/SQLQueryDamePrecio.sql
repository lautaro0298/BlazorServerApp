USE [BlazorUdemy]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDamePrecio]    Script Date: 18/12/2023 19:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[CursoDame](
@id int =null,
@NombreCurso varchar(500) =null,
@idalumno int =null
)AS
BEGIN
if(@idalumno is null)
begin
  SELECT Curso.id as idCurso, NombreCurso, Precio.id as idPrecio, Costo, Precio.fechaAlta, Precio.fechaBaja

 FROM Curso WITH(NOLOCK)
 INNER JOIN CursoPrecio WITH(NOLOCK) ON Curso.id=CursoPrecio.idCurso
 INNER JOIN Precio WITH(NOLOCK) ON CursoPrecio.idPrecio =Precio.id
 WHERE
 
 ( @id is null or Curso.id=@id) and (@NombreCurso is null or Curso.NombreCurso=@NombreCurso)
 order by Curso.id
end
BEGIN
 SELECT Curso.id as idCurso, Curso.NombreCurso, Precio.id as idPrecio, Precio.Costo, Precio.fechaAlta, Precio.fechaBaja
FROM Curso WITH(NOLOCK)
INNER JOIN CursoPrecio WITH(NOLOCK) ON Curso.id =CursoPrecio.idCurso
INNER JOIN Precio WITH(NOLOCK) ON CursoPrecio.idPrecio= Precio.id
WHERE idCurso not in(
SELECT distinct Curso.id
FROM Curso WITH(NOLOCK)
INNER JOIN CursoPrecio WITH(NOLOCK) ON Curso.id =CursoPrecio.idCurso
INNER JOIN Precio WITH(NOLOCK) ON CursoPrecio.idPrecio= Precio.id
INNER JOIN AlumnosCurso WITH(NOLOCK) ON Curso.id =AlumnosCurso.IdCurso
INNER JOIN Alumno WITH(NOLOCK) ON Alumno.id =AlumnosCurso.IdAlumno
where IdAlumno = @idalumno AND
FechaInscripcion BETWEEN Precio.fechaAlta AND Precio.fechaBaja
)
order by Curso. Id
END
end