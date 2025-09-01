using Microsoft.AspNetCore.Mvc;
using librarysite.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;


namespace librarysite.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string strcon = _configuration.GetConnectionString("con");

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    string query = "SELECT * FROM admin_login_tbl WHERE username=@username AND password=@password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", model.username);
                        cmd.Parameters.AddWithValue("@password", model.Password);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                TempData["LoginMessage"] = "success";
                                return RedirectToAction("MemberManagement", "Admin");
                            }
                            else
                            {
                                TempData["LoginMessage"] = "invalid";
                                return View(model);
                            }
                        }
                    }
                }
            }
            catch
            {
                TempData["LoginMessage"] = "error";
                return View(model);
            }
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
