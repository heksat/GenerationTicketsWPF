using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
{
    public partial class Discipline
    {
        public Discipline()
        {
            Teachings = new HashSet<Teaching>();
        }

        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public string SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<Teaching> Teachings { get; set; }
    }
}
