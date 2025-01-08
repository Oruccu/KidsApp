using System;
using KidsAppBackend.Data.Enums;

namespace KidsAppBackend.Data.Entities
{
    public class KidsMode : BaseEntity
    {
        public bool Boy { get; set; }
        public bool Girl { get; set; }
        public int ChildId { get; set; }
        public ChildUser Child { get; set; }
    }

}