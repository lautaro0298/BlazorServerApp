USE [BlazorUdemy]
GO
SET ANSI_NULLS ON
GO
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioBuscarAlumnos](
@texto varchar(500)
)as
BEGIN
Select * from dbo.Alumno
where nombre like '%'+@texto+'%'or
email like '%'+@texto+'%'
end