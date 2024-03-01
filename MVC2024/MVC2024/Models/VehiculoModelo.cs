using System.ComponentModel.DataAnnotations.Schema;

namespace MVC2024.Models
{
    public class VehiculoModelo
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Color { get; set; }
        public SerieModelo Serie { get; set;}
        public int SerieID { get; set;}

        public CarroceriaModelo Carroceria { get; set; }

        [NotMapped]

        public List<int> ExtrasSeleccionados { get; set; }

        public List<VehiculoExtraModelo> VehiculoExtras { get; set; }





    }
}
