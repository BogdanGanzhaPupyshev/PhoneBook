using AdressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdressBook.Controllers
{
    public class PhoneController : Controller
    {

        public ActionResult Index()
        {
            PhoneDBHandle dbHandle = new PhoneDBHandle();
            ModelState.Clear();
            return View(dbHandle.GetPhones());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Phone phone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PhoneDBHandle dbHandle = new PhoneDBHandle();
                    if (dbHandle.AddPhone(phone))
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
            PhoneDBHandle dbHandle = new PhoneDBHandle();
            return View(dbHandle.GetPhones().Find(person => person.PhoneId == id));
        }
        [HttpPost]
        public ActionResult Edit(Phone person)
        {
            try
            {
                PhoneDBHandle dbHandle = new PhoneDBHandle();
                dbHandle.UpdatePhoneDetails(person);
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
                PhoneDBHandle dbHandle = new PhoneDBHandle();
                if (dbHandle.DeletePhone(id))
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