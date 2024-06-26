USE [BlazorUdemy]
GO
/****** Object:  StoredProcedure [dbo].[CursoBorrar]    Script Date: 29/12/2023 02:02:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE[dbo].[CursoBorrar](
 @idCurso int
)AS
BEGIN

IF(SELECT count(*) FROM Curso WHERE id= @idCurso)=0
BEGIN 
RAISERROR('Curso no encontrado',16,1);
RETURN -1
END

IF(SELECT count(*) FROM AlumnosCurso WHERE id= @idCurso)>0
BEGIN

RAISERROR('Un curso con alumnos no puede ser borrado',16,1);
RETURN -1
END


DELETE FROM CursoPrecio where idCurso =@idCurso 
DELETE FROM Curso where id=@idCurso
end

