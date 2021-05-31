namespace DAL.Models
{
    public class Message : Entity
    {
        public AppUser Sender { get; set; }
        public AppUser Recipient { get; set; }
        public string Content { get; set; }
        public string DateTime { get; set; }

    }
}
