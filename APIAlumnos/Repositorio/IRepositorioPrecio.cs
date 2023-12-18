using LibreriaClases;

namespace APIlumnos.Repositorio
{
    public interface IRepositorioPrecio
    {
        Task<Precio> AltaPrecio(Precio Precio);

        Task<IEnumerable<Precio>> DamePrecios();

        Task<Precio> DamePrecios(int id);
        Task<Precio> DamePrecios(string email);
        Task<Precio> ModificarPrecio(Precio Precio);

        Task<Precio> BorrarPrecio(int id);
    }
}
