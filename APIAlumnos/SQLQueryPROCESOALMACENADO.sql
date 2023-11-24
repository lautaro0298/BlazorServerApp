CREATE PROCEDURE [dbo].[UsuarioDameAlumnos](
@id int =null
)AS
BEGIN
Select * FROM Alumno WITH(NOLOCK) WHERE @id is null or id=@id
end