namespace Recap.Core.Entities.Concrete
{
    public class User:BaseModel, IEntity
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; } 

        public string Password { get; set; }


        public int DepartmanId { get; set; }
        public int UnvanId { get; set; }

    }

    
}
