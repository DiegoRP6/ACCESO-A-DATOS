using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EXAMENAPI2;

namespace EXAMENAPI2.Controllers
{
    public class CursosController : ApiController
    {
        public IEnumerable<Cursos> Get()
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Cursos.ToList();
            }
        }
        public Cursos Get(int id)
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Cursos.Find(id);
            }
        }
    }
}
