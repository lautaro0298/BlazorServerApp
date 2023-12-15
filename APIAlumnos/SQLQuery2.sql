USE [BlazorUdemy]
GO
SET ANSI_NULLS ON
GO
ALter PROCEDURE[dbo].[UsuarioAltaAlumno](
@nombre varchar(500),
@email varchar(500),
@foto varchar(500),
@fechaAlta datetime=null,
@idAlumno int = null,
@fechaBaja datetime=null
)AS

BEGIN
if(@idAlumno is null)
begin
INSERT INTO dbo.Alumno(nombre,email,foto,fechaAlta)
Values(@nombre,@email,@foto,@fechaAlta)
Select @@IDENTITY as idAlumno
end
else
begin
	UPDATE dbo.Alumno SET nombre= @nombre, email = @email,foto=@foto,fechaBaja=@fechaBaja
	where id=@idAlumno
	select @idAlumno as idAlumno
	end
end