using AdressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdressBook.Controllers
{
    public class PhoneBookController : Controller
    {
        public ActionResult Index()
        {
            PhoneBookDBHandle dbHandle = new PhoneBookDBHandle();
            ModelState.Clear();
            return View(dbHandle.GetPhoneBook());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PhoneBook phoneBook)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PhoneBookDBHandle dbHandle = new PhoneBookDBHandle();
                    if (dbHandle.AddPhoneBookRecord(phoneBook))
                    {
                        ViewBag.Message = "PhoneBook Recorcd Added";
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

        public ActionResult Delete(int id)
        {
            try
            {
                PhoneBookDBHandle dbHandle = new PhoneBookDBHandle();
                if (dbHandle.DeletePhoneBookRecord(id))
                {
                    ViewBag.DeleteMessage = "Record deleted";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}