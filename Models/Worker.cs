using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Teachings = new HashSet<Teaching>();
        }

        public int WorkerId { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string Gender { get; set; }
        public string WorkerLogin { get; set; }
        public string WorkerPassword { get; set; }
        
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Teaching> Teachings { get; set; }
    }
}
