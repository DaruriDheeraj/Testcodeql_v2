using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;

public class AuthController : Controller
{
    private readonly UserRepository _userRepo;

    public AuthController(UserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _userRepo.GetUserByUsername(username);

        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Dashboard");
        }

        ViewBag.Message = "Invalid credentials";
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Username = username,
            PasswordHash = hashedPassword
        };

        _userRepo.AddUser(user);
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("UserId") == null)
        {
            return RedirectToAction("Login");
        }

        ViewBag.Username = HttpContext.Session.GetString("Username");
        return View();
    }
}