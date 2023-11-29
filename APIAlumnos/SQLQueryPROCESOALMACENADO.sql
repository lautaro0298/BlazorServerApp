ALTER PROCEDURE [dbo].[UsuarioDameAlumnos](
@id int =null,
@email varchar(500) =null
)AS
BEGIN
Select * FROM Alumno WITH(NOLOCK) WHERE ( @id is null or id=@id) and (@email is null or email=@email)
end