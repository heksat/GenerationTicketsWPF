using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
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
        public int DisciplineId { get; set; }
        public string WorkerLogin { get; set; }
        public string WorkerPassword { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Teaching> Teachings { get; set; }
    }
}
