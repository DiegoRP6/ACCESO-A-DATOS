using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EXAMENAPI2;

namespace EXAMENAPI2.Controllers
{
    public class AlumnoController : ApiController
    {
        public IEnumerable<Alumnos> Get()
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Alumnos.ToList();
            }
        }

        public Alumnos Get(int id)
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Alumnos.Find(id);
            }
        }

        [AcceptVerbs("GET")]
        public IEnumerable<Alumnos> Get2(int id)
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Alumnos.Where(x => x.CursoID == id).ToList();
            }
        }
    }
}