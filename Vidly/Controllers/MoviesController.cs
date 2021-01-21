using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Movies/New")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genre = _context.Genres.ToList();
            var movie = new Movie(); // initialize the datetime and nbStock
            var viewmodel = new MovieFormViewModel(movie)
            {
                Genre = genre
            };
            return View("MovieForm",viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new MovieFormViewModel (movie)
                {
                    Genre = _context.Genres.ToList()
                };

                return View("MovieForm", viewmodel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            } 

            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock  = movie.NumberInStock;
            }

                _context.SaveChanges();

            return RedirectToAction("index", "Movies");
        }

        // GET: Movie/Random

        public ActionResult Random()
        {
            //return View(movie);
            //return Content("jugurtha");   
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("index", "home", new { page = "1", SortedBy = "name" });
            /*
            var movie = new Movie { name = "Shrak!" };
            var customers = new List<Customer>
            {
                new Customer {name ="customer 1" },
                new Customer { name ="customer 2"}
            };

            var viewmodel = new RandomViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewmodel);
        }

        public ActionResult Edit (int id)
        {
            return Content("id =" +id);
        }

        //movies
        public ActionResult index (int? page, string SortedBy)
        {
            if (!page.HasValue)
                page = 1;
            if (string.IsNullOrWhiteSpace(SortedBy))
                SortedBy = "name";
            return Content(string.Format("page={0},SortedBy={1}",page, SortedBy));
        }

        //implimente the attribue route
        [Route("movies/released/{year:regex(2015|2016)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult byReleasedYear(int year, int month)
        {
            return Content(year+"/"+month);
        }*/
            return null;
        }
        //movie
        public ActionResult index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            return View("List");

            return View("ReadOnlyList");
        }


        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewmodel = new MovieFormViewModel (movie)
            {
                Genre = _context.Genres.ToList()
            };
            return View("MovieForm",viewmodel);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

      
    }
}