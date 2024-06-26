USE [BlazorUdemy]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioInscriptoCursos]    Script Date: 22/1/2024 04:23:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UsuarioInscriptoCursos](
@idAlumno int)as
begin
Select distinct Alumno.id,nombre,email,foto,Curso.id as idcurso,NombreCurso ,Precio.Costo,Precio.fechaAlta,Precio.fechaBaja
INTO #TMP
from Alumno with (NOLOCK)
INNER JOIN AlumnosCurso With (NOLOCK) on AlumnosCurso.idAlumno =Alumno.id
INNER JOIN Curso ON Curso.id=AlumnosCurso.idCurso
INNER JOIN CursoPrecio ON CursoPrecio.idCurso =Curso.id 
INNER JOIN Precio ON Precio.id=CursoPrecio.idPrecio
where idAlumno=@idAlumno
order by Curso.id 
SELECT distinct #TMP.id,nombre,email,foto, AlumnosCurso.idCurso , NombreCurso,Costo,fechaAlta,fechaBaja From #TMP
INNER JOIN AlumnosCurso ON AlumnosCurso.IdAlumno= #TMP.id
WHERE FechaInscripcion BETWEEN fechaAlta AND fechaBaja AND AlumnosCurso.IdCurso =#TMP.idcurso
order by AlumnosCurso.idCurso
DROP TABLE #TMP
end