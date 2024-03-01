using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EXAMENAPI2;

namespace EXAMENAPI2.Controllers
{
    public class CiclosController : ApiController
    {
        public IEnumerable<Ciclos> Get()
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Ciclos.ToList();
            }
        }
        public Ciclos Get(int id)
        {
            using (ExamenEntities colegio = new ExamenEntities())
            {
                return colegio.Ciclos.Find(id);
            }
        }
    }
}
