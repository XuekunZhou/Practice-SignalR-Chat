#nullable disable
namespace ChatWebApp.Models
{
    public class ApplicationUserChat
    {
        public string ChatsId { get; set; }
        public Chat Chat { get; set; }

        public string ParticipantsId { get; set; }
        public ApplicationUser Participant { get; set; }
    }
}