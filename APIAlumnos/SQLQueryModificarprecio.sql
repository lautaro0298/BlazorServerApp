USE [BlazorUdemy]
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE[dbo].[CursoModificar](
 @idCurso int,
 @idPrecio int,
@NombreCurso varchar(500),
@Costo float,
@fechaAlta datetime=null,
@fechaBaja datetime=null
)AS
begin
DECLARE @idPrecioNuevo int

IF(SELECT count(*) FROM Curso WHERE id= @idCurso)=0
BEGIN 
RAISERROR('Curso no encontrado',16,1);
RETURN -1
END

IF(SELECT count(*) FROM Precio WHERE id= @idPrecio)=0
BEGIN

RAISERROR('Precio no encontrado',16,1);
RETURN -1
END

UPDATE dbo.Curso SET NombreCurso = GETDATE()WHERE id=@idCurso
DELETE FROM CursoPrecio where idPrecio=@idPrecio and idCurso =@idCurso 
INSERT INTO dbo.Precio(Costo,fechaAlta,fechaBaja)Values(@Costo,@fechaAlta,@fechaBaja)
	Set @idPrecioNuevo = @@IDENTITY 
INSERT INTO dbo.CursoPrecio(idCurso,idPrecio)Values(@idCurso,@idPrecioNuevo)
end