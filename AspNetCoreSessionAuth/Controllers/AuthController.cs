using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    // Dummy username and password for testing
    private const string DummyUsername = "admin";
    private const string DummyPassword = "password122";  // Dummy password

    // GET: Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Compare entered username and password with dummy credentials
        if (username == DummyUsername && password == DummyPassword)
        {
            // Successful login - redirect to a different page (e.g., Dashboard)
            return RedirectToAction("Dashboard");
        }
        else
        {
            // If credentials are incorrect, show an error message
            ViewBag.Message = "Invalid username or password.";
            return View();
        }
    }

    // Dashboard (dummy)
    public IActionResult Dashboard()
    {
        return View();
    }
}
