using QuantumSchool.DAL;
using System.Web.Mvc;

namespace QuantumSchool.Controllers {
    public class ControllerBase : Controller {
        protected SchoolContext db = new SchoolContext();
        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}