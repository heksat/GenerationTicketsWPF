using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Discipline
    {
        public Discipline()
        {
            Teachings = new ObservableCollection<Teaching>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public string SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ObservableCollection<Teaching> Teachings { get; set; }
    }
}
