using System;

namespace OilTeamProject.Models.Products
{
    public class Message
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}