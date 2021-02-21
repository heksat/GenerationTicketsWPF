using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
{
    public partial class TypesTask
    {
        public TypesTask()
        {
            Tasks = new HashSet<Task>();
        }

        public int TypesTaskId { get; set; }
        public string TypesTaskDecryption { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
