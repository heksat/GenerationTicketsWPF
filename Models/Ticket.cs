using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public byte TaskNumber { get; set; }

        public int TaskId { get; set; }
        public int DisciplineId { get; set; }

        public int ChairmanId { get; set; }

        public virtual Chairman Chairman { get; set; }
        public virtual Task Task { get; set; }
    }
}
