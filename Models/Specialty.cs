using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Disciplines = new HashSet<Discipline>();
            SpecialtyId = "00.00.00";
        }

        public string SpecialtyId { get; set; }
        public string SpecialtyDecryption { get; set; }
        public int ChairmanId { get; set; }

        public virtual Chairman Chairman { get; set; }
        public virtual ICollection<Discipline> Disciplines { get; set; }
    }
}
