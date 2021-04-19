using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Disciplines = new ObservableCollection<Discipline>();
        }

        public string SpecialtyId { get; set; }
        public string SpecialtyDecryption { get; set; }
        public int ChairmanId { get; set; }

        public virtual Chairman Chairman { get; set; }
        public virtual ObservableCollection<Discipline> Disciplines { get; set; }
    }
}
