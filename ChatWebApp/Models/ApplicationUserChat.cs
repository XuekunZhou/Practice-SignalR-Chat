#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ChatWebApp.Models
{
    public class ApplicationUserChat
    {
        [Key]
        public int Id { get; set; }
        
        public ApplicationUser ApplicationUsers { get; set; }
        public string ApplicationUserId { get; set; }

        public Chat Chats { get; set; }
        public string ChatsId { get; set; }

        public DateTime Joined { get; set; }
    }
}