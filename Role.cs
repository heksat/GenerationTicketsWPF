using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
{
    public partial class Role
    {
        public Role()
        {
            Workers = new HashSet<Worker>();
        }

        public int RoleId { get; set; }
        public string RoleDecryption { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
