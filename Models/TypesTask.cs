using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
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
