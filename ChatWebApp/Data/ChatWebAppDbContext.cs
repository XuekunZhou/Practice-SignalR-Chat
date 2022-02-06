#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ChatWebAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public ChatWebAppDbContext (DbContextOptions<ChatWebAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatWebApp.Models.Chat> Chats { get; set; }
        public DbSet<ChatWebApp.Models.ChatMessage> ChatMessages { get; set; }
    }
