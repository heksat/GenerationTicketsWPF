using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
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
