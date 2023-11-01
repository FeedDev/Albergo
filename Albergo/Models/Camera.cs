namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Camera")]
    public partial class Camera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Camera()
        {
            Prenotazione = new HashSet<Prenotazione>();
        }

        [Key]
        public int IdCamera { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Codice Camera")]
        public string CodiceCamera { get; set; }

        public string Descrizione { get; set; }

        [Required]
        [StringLength(30)]
        public string Tipologia { get; set; }

        public int? Piano { get; set; }

        public bool Cancellato { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prenotazione> Prenotazione { get; set; }
    }
}
