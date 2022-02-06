#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatWebApp.Models
{
    public class Chat
    {
        [Key]
        public string Id { get; set; }
 
        public string Name { get; set; }
        public ICollection<ApplicationUser> Participants { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}