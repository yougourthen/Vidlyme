using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //New Customer Form
        public ActionResult New()
        {
            var membershipeTypes = _context.MembershipeTypes.ToList();
            var viewmodel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipeTypes
            };

            return View("CustomerForm", viewmodel);
        }

        //Add Customer To Db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipeTypes.ToList()
                };
                return View("CustomerForm", viewmodel);
            }

            if (customer.Id == 0)
            _context.Customers.Add(customer); //Add Customer to Memory

             else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeID = customer.MembershipTypeID;
                customerInDb.IsSubscriebedToNewsLetter = customer.IsSubscriebedToNewsLetter;
                customerInDb.BirthDate = customer.BirthDate;
            }

            _context.SaveChanges();          //Save the Changes in DB  

            return RedirectToAction ("index", "Customers"); 
        }

        // GET: Customers
        [Route("Customers")]
        public ActionResult index()
        {
            return View();
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();


            var viewmodel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipeTypes.ToList()
            };

            return View("CustomerForm", viewmodel);
        }



    }
}