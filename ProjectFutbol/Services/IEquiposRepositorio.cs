using Microsoft.AspNetCore.Http;
using Modelos; // Importing the namespace where the 'Equipo' class is defined

namespace Services
{
    // Declaration of the interface named IEquiposRepositorio
    public interface IEquiposRepositorio
    {
        // Declaration of a method named GetEquipos that returns IEnumerable<Equipo>
        IEnumerable<Equipo> GetEquipos();

        // Declaration of a method named GetEquipo that returns Equipo and receives an int as parameter
        Equipo GetEquipo(int id);

        // Declaration of a method named Update that returns Equipo and receives an Equipo as parameter
        Equipo Update(Equipo equipoActualizado);

        // Metodo que reciba un archivo y lo inserte en equipo.foto
        void InsertarFoto(IFormFile photo);

        //Metodo busqueda de equipos
        IEnumerable<Equipo> BuscarEquipos(string elementoABuscar);

        //Metodo equipos por categoria
        IEnumerable<CategoriaCuantos> equiposPorCategoria();
         
        //Metodo para insertar un equipo
        Equipo InsertarEquipo(Equipo equipoNuevo);

        //metodo para cambiar foto
        void CambiarFoto(IFormFile photo, int id);


    }
}