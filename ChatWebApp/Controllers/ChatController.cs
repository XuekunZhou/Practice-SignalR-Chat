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

        public async Task<IActionResult> IndexAsync(string? id)
        {   
            var loggedInUser = await _userManager.GetUserAsync(User);
            var chat = _context.Chats.Where(c => c.Id == id).SingleOrDefault();

            if (chat != null)
            {
                var chatList = _context.ApplicationUserChat.Where(u => u.Participant == loggedInUser);
                
                bool access = false;
                while (!access)
                {
                    foreach(var applicationUserChat in chatList)
                    {
                        if (applicationUserChat.ChatsId == id)
                        {
                            access = true;
                        }
                    }
                }
                if (access)
                {
                    return View();
                }
            }
            return RedirectToAction("Error", "NoAccess");
        }
    }
}