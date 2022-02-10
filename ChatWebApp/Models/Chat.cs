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
        public List<ApplicationUser> Participants { get; set; }
        public List<ApplicationUserChat> ApplicationUserChats { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public bool Public { get; private set; }

        public Chat()
        {

        }

        public Chat(string name)
        {
            Id = CreateId();
            Name = name;
            Public = true;
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