using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Teaching
    {
        public Teaching()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int DisciplineId { get; set; }

        [Key]
        public int WorkerId { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
