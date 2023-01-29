namespace WebAplicationMentum.Models
{
    public class CustomerContactModel
    {
        public int Id { get; set; }
        public CustomersModel customersModel { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string NumberPhone { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
