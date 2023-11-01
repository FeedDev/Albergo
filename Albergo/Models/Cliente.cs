namespace Albergo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Prenotazione = new HashSet<Prenotazione>();
        }

        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Codice Fiscale")]
        public string CodiceFiscale { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(50)]
        public string Città { get; set; }

        [Required]
        [StringLength(40)]
        public string Provincia { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }

        [StringLength(30)]
        public string Cellulare { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prenotazione> Prenotazione { get; set; }
    }
}
