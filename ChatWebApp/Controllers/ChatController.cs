using System.Linq;
using ChatWebApp.Hubs;
using ChatWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatWebApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ChatWebAppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(ChatWebAppDbContext context, UserManager<ApplicationUser> userManager, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var chats = _context.Chats.ToList();
            return View(chats);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ChatViewModel model)
        {
            if (ModelState.IsValid)
            {
                var chat = new Chat(model.Name);
                _context.Add(chat);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAsync(string id)
        {   
            var chat = _context.Chats.Where(c => c.Id == id).SingleOrDefault();

            if (chat != null)
            {
                ViewData["participates"] = await participatesAsync(chat);
                return View(chat);
            }
            
            return RedirectToAction("NotFound", "Error");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var chat = _context.Chats.Where(c => c.Id == id).SingleOrDefault();

            if (chat != null)
            {
                _context.Chats.Remove(chat);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinAsync(string id)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var chat = _context.Chats.Where(c => c.Id == id).SingleOrDefault();

            if (chat != null)
            {
                var participates = new ApplicationUserChat
                {
                    ApplicationUsers = loggedInUser,
                    ApplicationUserId = loggedInUser.Id,
                    Chats = chat,
                    ChatsId = chat.Id,
                    Joined = DateTime.UtcNow
                };

                _context.Add(participates);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new {id});
        }

        [HttpGet]
        public async Task<IActionResult> LeaveAsync(string id)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var chat = _context.Chats.Where(c => c.Id == id).SingleOrDefault();

            if (chat != null)
            {
                var participates = _context.ApplicationUserChats.Where(u => u.ApplicationUsers == loggedInUser && u.Chats == chat).SingleOrDefault();
                
                if (participates != null)
                {
                    _context.ApplicationUserChats.Remove(participates);
                    _context.SaveChanges();
                }
            }
            
            return RedirectToAction("Details", new {id});
        }

        [HttpGet]
        public async Task<IActionResult> ChatroomAsync(string id)
        {
            var chat = _context.Chats.Where(c => c.Id == id).SingleOrDefault();
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (chat != null)
            {
                ViewData["user"] = loggedInUser.Id;
                return View(chat);
            }
            
            return RedirectToAction("NotFound", "Error");
        }

        private async Task<bool> participatesAsync(Chat chat)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var participants = _context.ApplicationUserChats.Where(ac => ac.ChatsId == chat.Id).Select(au => au.ApplicationUsers);

            if (participants.Contains(loggedInUser))
            {
                return true;
            }

            return false;
        }
    }
}