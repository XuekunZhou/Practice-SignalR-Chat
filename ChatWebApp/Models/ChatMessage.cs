#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ChatWebApp.Models
{
    public class ChatMessage{
        [Key]
        public string Id { get; set; }

        public ApplicationUser Sender { get; set; }
        public string SenderId { get; set; }
        
        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public Chat Chat { get; set; }
        public string ChatId { get; set; }
    }
}