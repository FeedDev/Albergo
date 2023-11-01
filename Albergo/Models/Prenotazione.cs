namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prenotazione")]
    public partial class Prenotazione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prenotazione()
        {
            PivotPrenotazioneServizio = new HashSet<PivotPrenotazioneServizio>();
        }

        [Key]
        public int IdPrenotazione { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Prenotazione")]
        public DateTime DataPrenotazione { get; set; }

        public int CodicePrenotazione { get; set; }

        [Required]
        [StringLength(10)]
        public string Anno { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Inizio Soggiorno")]
        public DateTime InizioSoggiorno { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fine Soggiorno")]
        public DateTime FineSoggiorno { get; set; }

        [Column(TypeName = "money")]
        public decimal Caparra { get; set; }

        [Column(TypeName = "money")]
        public decimal Tariffa { get; set; }

        [Display(Name = "Tipologia")]
        public int IdTipologiaFk { get; set; }

        [Display(Name = "Codice Fiscale")]
        public int IdClienteFk { get; set; }

        public bool Cancellato { get; set; }

        [Display(Name = "Camera")]
        public int IdCameraFk { get; set; }

        public virtual Camera Camera { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PivotPrenotazioneServizio> PivotPrenotazioneServizio { get; set; }
        public virtual Tipologia Tipologia { get; set; }
    }
}
