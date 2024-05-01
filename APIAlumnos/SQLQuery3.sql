GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UsuarioMarcaBaja](
@IdAlumno int
)as
BEGIN
IF(SELECT count(*) FROM Alumno WHERE id=@IdAlumno and fechaBaja is not null)=1
begin
RAISERROR('Este alumno ya esta de baja',16,1);
RETURN -1
END
UPDATE dbo.Alumno SET fechaBaja = GETDATE()WHERE id=@idAlumno

SELECT @idAlumno as idAlumno


end