using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Equipo
    {
        public int Id { get; set; }
        public string nomEquipo { get; set; }
        public string Ciudad { get; set; }
        public string nomEstadio { get; set; }

        public string Foto { get; set; }
        public Categoria categoria { get; set; }
    }
}
