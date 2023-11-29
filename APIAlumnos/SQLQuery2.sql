USE [BlazorUdemy]
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE[dbo].[UsuarioAltaAlumno](
@nombre varchar(500),
@email varchar(500),
@foto varchar(500),
@fechaAlta datetime
)AS
BEGIN
INSERT INTO dbo.Alumno(nombre,email,foto,fechaAlta)
Values(@nombre,@email,@foto,@fechaAlta)
Select @@IDENTITY as idAlumno
end