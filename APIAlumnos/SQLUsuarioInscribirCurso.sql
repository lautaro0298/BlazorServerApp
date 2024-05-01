alter PROCEDURE [dbo].[UsuarioInscribirCurso](@idAlumno int,
@idcurso int
)as
begin
IF(SELECT count(*) FROM Curso WHERE id= @idCurso)=0
BEGIN 
RAISERROR('Curso no encontrado',16,1);
RETURN -1
END

IF(SELECT count(*) FROM Alumno WHERE id= @idAlumno)=0
BEGIN

RAISERROR('Alumno no encontrado',16,1);
RETURN -1
END
IF(SELECT count(*) FROM AlumnosCurso WHERE idAlumno= @idAlumno and idCurso=@idcurso)>0
begin
	RAISERROR('Este alumno ya esta inscripto en este curso',16,1);
RETURN -1
END
Insert into dbo.AlumnosCurso(idAlumno,idCurso)Values(@idAlumno,@idcurso)
end