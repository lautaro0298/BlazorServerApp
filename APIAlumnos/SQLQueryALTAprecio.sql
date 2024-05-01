USE [BlazorUdemy]
GO
SET ANSI_NULLS ON
GO
ALTER PROCEDURE[dbo].[CursoAlta](
@Nombrecurso varchar(500),
@Costo float,
@fechaAlta datetime=null,
@fechaBaja datetime=null
)AS

DECLARE @idCurso int
DECLARE @idPrecio int
DECLARE @idRespuesta int

INSERT INTO dbo.Curso (NombreCurso)Values (@Nombrecurso)
SET @idCurso =@@IDENTITY

SELECT @idPrecio=id FROM Precio WHERE Costo=@Costo and fechaAlta=@fechaAlta and fechaBaja=@fechaBaja

BEGIN

if(@idPrecio is null)
begin
	INSERT INTO dbo.Precio(Costo,fechaAlta,fechaBaja)Values(@Costo,@fechaAlta,@fechaBaja)
	SET @idPrecio =@@IDENTITY
end

INSERT INTO dbo.CursoPrecio(idCurso,idPrecio)Values(@idCurso,@idPrecio)
Set @idRespuesta =@idCurso
Select @idRespuesta as IdCurso

end