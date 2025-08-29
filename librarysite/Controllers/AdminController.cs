using librarysite.Models;
using Microsoft.AspNetCore.Mvc;

namespace librarysite.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AuthorManagement()
        {
            return View(new AuthorViewModel());
        }

        public IActionResult PublisherManagement()
        {
            return View(new PublisherViewModel());
        }

        public IActionResult BookInventory()
        {
            return View();
        }

        public IActionResult BookIssue()
        {
            return View();
        }

        public IActionResult MemberManagement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchAuthor(int AuthorId)
        {
            var model = new AuthorViewModel { Authorid = AuthorId };

            // DB lookup
            var authorName = GetAuthorNameById(AuthorId);

            if (!string.IsNullOrEmpty(authorName))
            {
                model.Authorname = authorName;
            }
            else
            {
                ViewBag.Message = "Author not found.";
            }

            return View("AuthorManagement", model);
        }

        private string GetAuthorNameById(int id)
        {
            var authors = new Dictionary<int, string>
            {
                { 101, "J.K. Rowling" },
                { 102, "George Orwell" },
                { 103, "Chinua Achebe" }
            };

            return authors.ContainsKey(id) ? authors[id] : null;
        }

        [HttpGet]
        public IActionResult SearchPublisher(int PublisherId)
        {
            var model = new PublisherViewModel { Publisherid = PublisherId };

            // DB lookup
            var publisherName = GetPublisherNameById(PublisherId);

            if (!string.IsNullOrEmpty(publisherName))
            {
                model.Publishername = publisherName;
            }
            else
            {
                ViewBag.Message = "Author not found.";
            }

            return View("AuthorManagement", model);
        }

        private string GetPublisherNameById(int id)
        {
            var publishers = new Dictionary<int, string>
            {
                { 101, "J.K. Rowling" },
                { 102, "George Orwell" },
                { 103, "Chinua Achebe" }
            };

            return publishers.ContainsKey(id) ? publishers[id] : null;
        }

    }
}
