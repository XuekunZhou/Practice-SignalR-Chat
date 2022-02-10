#nullable disable
using Microsoft.AspNetCore.Identity;

namespace ChatWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public List<ApplicationUserChat> ApplicationUserChats { get; set; }
        public List<ChatMessage> SentMessages { get; set; }

        public ApplicationUser()
        {
            
        }

        public ApplicationUser(string firstName, string lastName, DateTime dateOfBirth, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            UserName = email;
        }
    }
}