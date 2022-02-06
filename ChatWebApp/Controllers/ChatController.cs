using ChatWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatWebAppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(ChatWebAppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string? id)
        {
            return View();
        }
    }
}