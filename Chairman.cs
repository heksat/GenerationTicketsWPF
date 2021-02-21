﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GenerationTicketsWPF
{
    public partial class Chairman
    {
        public Chairman()
        {
            Specialties = new HashSet<Specialty>();
            Tickets = new HashSet<Ticket>();
        }

        public int ChairmanId { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }

        public virtual ICollection<Specialty> Specialties { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
