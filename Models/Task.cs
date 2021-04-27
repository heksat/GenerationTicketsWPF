using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Task
    {
        public Task()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TaskId { get; set; }
        public int DisciplineId { get; set; }
        public string TaskDecryption { get; set; }
        public int LevelId { get; set; }
        public int TypesTaskId { get; set; }
        public int WorkerId { get; set; }

        public virtual Level Level { get; set; }
        public virtual Teaching Teaching { get; set; }
        public virtual TypesTask TypesTask { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
