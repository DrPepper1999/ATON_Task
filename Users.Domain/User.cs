using System;

namespace Users.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool Admin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime RevokedOn { get; set; }
        public string RevokedBy { get; set; }

        public User()
        {
            Login = String.Empty;
            Password = String.Empty;
            Name = String.Empty;
            CreatedBy = string.Empty;
            ModifiedBy = string.Empty;
            RevokedBy = string.Empty;
        }
    }
}
