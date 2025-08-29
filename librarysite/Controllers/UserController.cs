using Microsoft.AspNetCore.Mvc;
using librarysite.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

    
namespace librarysite.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Signup()
        {
            ViewBag.Greeting = "Welcome to the Signup Page!";
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (CheckMemberIdExists(model.MemberId))
            {
                ModelState.AddModelError("MemberId", "Member ID already exists.");
                return View(model);
            }
            else
            {
                SignupNewUser(model);
                return RedirectToAction("Login", "User");
            }

        }

        bool CheckMemberIdExists(string memberid)
        {
            string strcon = _configuration.GetConnectionString("con");
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM member_master_tbl WHERE member_id=@member_id", con);
                    cmd.Parameters.AddWithValue("@member_id", memberid);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                TempData["SignupSuccess"] = "Something went wrong. Please try again.";
                return false;
            }
        }

        public void SignupNewUser(SignupViewModel model)
        {
            string strcon = _configuration.GetConnectionString("con");
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO member_master_tbl
                (full_name, dob, contact_no, email, state, city, pincode, full_address, member_id, password, account_status)
                VALUES
                (@full_name, @dob, @contact_no, @email, @state, @city, @pincode, @full_address, @member_id, @password, @account_status)", con);

                    cmd.Parameters.AddWithValue("@full_name", model.Name.Trim());
                    cmd.Parameters.AddWithValue("@dob", model.DOB);
                    cmd.Parameters.AddWithValue("@contact_no", model.Contact);
                    cmd.Parameters.AddWithValue("@email", model.Email ?? "");
                    cmd.Parameters.AddWithValue("@state", model.State ?? "");
                    cmd.Parameters.AddWithValue("@city", model.City ?? "");
                    cmd.Parameters.AddWithValue("@pincode", model.Pincode);
                    cmd.Parameters.AddWithValue("@full_address", model.Address ?? "");
                    cmd.Parameters.AddWithValue("@member_id", model.MemberId);
                    cmd.Parameters.AddWithValue("@password", model.Password);
                    cmd.Parameters.AddWithValue("@account_status", "pending");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Database error. Please try again.");
            }

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string strcon = _configuration.GetConnectionString("con");
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id=@member_id AND password=@password", con);
                    cmd.Parameters.AddWithValue("@member_id", model.MemberId);
                    cmd.Parameters.AddWithValue("@password", model.Password);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string memberId = dr["member_id"].ToString();
                            string fullName = dr["full_name"].ToString();
                            TempData["LoginMessage"] = "success";
                        }
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        TempData["LoginMessage"] = "invalid";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["LoginMessage"] = "error";
                return View(model);
            }
                    return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Hellouser()
        {
            return View();
        }
    }
}