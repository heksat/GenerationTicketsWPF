using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
{
    public partial class Level
    {
        public Level()
        {
            Tasks = new HashSet<Task>();
        }

        public int LevelId { get; set; }
        public string LeverDecryption { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
