using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
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
