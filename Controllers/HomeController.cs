using AdressBook.Models;
using System.Web.Mvc;

namespace AdressBook.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            PersonDBHandle dbHandle = new PersonDBHandle();
            ModelState.Clear();
            return View(dbHandle.GetPerson());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    PersonDBHandle dbHandle = new PersonDBHandle();
                    if(dbHandle.AddPerson(person))
                    {
                        ViewBag.Message = "Person Add";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            PersonDBHandle dbHandle = new PersonDBHandle();
            return View(dbHandle.GetPerson().Find(person => person.PersonId == id));
        }
        [HttpPost]
        public ActionResult Edit(Person person)
        {
            try
            {
                PersonDBHandle dbHandle = new PersonDBHandle();
                dbHandle.UpdateDetails(person);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                PersonDBHandle dbHandle = new PersonDBHandle();
                if(dbHandle.DeletePerson(id))
                {
                    ViewBag.DeleteMessage = "Person deleted";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}