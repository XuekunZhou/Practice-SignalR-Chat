#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ChatWebApp.Models
{
    public class ChatViewModel
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}