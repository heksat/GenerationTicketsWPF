using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace GenerationTicketsWPF
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
        [Browsable(false)]
        public virtual Level Level { get; set; }
        [Browsable(false)]
        public virtual Teaching Teaching { get; set; }
        [Browsable(false)]
        public virtual TypesTask TypesTask { get; set; }
        [Browsable(false)]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
