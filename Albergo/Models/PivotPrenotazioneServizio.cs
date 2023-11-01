namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PivotPrenotazioneServizio")]
    public partial class PivotPrenotazioneServizio
    {
        [Key]
        public int IdPivotPrenotazioneServizio { get; set; }

        public int IdPrenotazione { get; set; }

        public int IdServizio { get; set; }

        public virtual Prenotazione Prenotazione { get; set; }

        public virtual Servizio Servizio { get; set; }

        public int Quantità { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Servizio")]
        public DateTime DataServizio { get; set; }

        public bool Cancellato { get; set; }
    }
}
