using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
{
    public partial class Teaching
    {
        public Teaching()
        {
            Tasks = new HashSet<Task>();
        }

        public int DisciplineId { get; set; }
        public int WorkerId { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
