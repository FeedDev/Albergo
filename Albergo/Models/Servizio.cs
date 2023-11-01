namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servizio")]
    public partial class Servizio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servizio()
        {
            PivotPrenotazioneServizio = new HashSet<PivotPrenotazioneServizio>();
        }

        [Key]
        public int IdServizio { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome Servizio")]
        public string NomeServizio { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        

        public bool Cancellato { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PivotPrenotazioneServizio> PivotPrenotazioneServizio { get; set; }
    }
}
