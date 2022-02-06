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

        public Chat()
        {

        }

        public Chat(string name)
        {
            Id = CreateId();
            Name = name;
        }

        private static string CreateId()
        {
            string id = "c";

            var date = DateTime.UtcNow.ToString();

            id += date.Replace("/", "").Replace(" ", "").Replace(":", "");

            return id;
        }
    }
}