using System;
using KidsAppBackend.Data.Enums;

namespace KidsAppBackend.Data.Entities
{
    public class ParentUser : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<ChildUser> Children { get; set; }
    }
}