using System;

namespace Models
{
  public class User : BaseEntity//, IAggregateRoot
  {
    public User()
    {

    }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastNAme { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
