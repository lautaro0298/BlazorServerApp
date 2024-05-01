USE [BlazorUdemy]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[CursoBorrar](
 @idCurso int
)AS
BEGIN

IF(SELECT count(*) FROM Curso WHERE id= @idCurso)=0
BEGIN 
RAISERROR('Curso no encontrado',16,1);
RETURN -1
END

IF(SELECT count(*) FROM AlumnosCurso WHERE id= @idCurso)=0
BEGIN

RAISERROR('Curso no encontrado',16,1);
RETURN -1
END


DELETE FROM CursoPrecio where idCurso =@idCurso 
DELETE FROM Curso where id=@idCurso
end

