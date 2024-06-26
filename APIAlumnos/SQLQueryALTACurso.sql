USE [BlazorUdemy]
GO
/****** Object:  StoredProcedure [dbo].[CursoAlta]    Script Date: 20/1/2024 17:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
SELECT @idCurso=id From Curso WITH(NOLOCK)WHEre NombreCurso=@Nombrecurso
if(@idCurso is null)
begin
	INSERT INTO dbo.Curso (NombreCurso)Values (@Nombrecurso)
	SET @idCurso =@@IDENTITY
END
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