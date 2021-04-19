using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Chairman
    {
        public Chairman()
        {
            Specialties = new ObservableCollection<Specialty>();
            Tickets = new ObservableCollection<Ticket>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChairmanId { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }

        public virtual ObservableCollection<Specialty> Specialties { get; set; }
        public virtual ObservableCollection<Ticket> Tickets { get; set; }
        
    }
}
